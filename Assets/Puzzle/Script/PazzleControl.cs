using UnityEngine;
using System.Collections;

public class PazzleControl : MonoBehaviour
{

    public GameControl game_control = null;

    private int piece_num;              // 조각의 수（전부）.
    private int piece_finished_num;     // 완성한 조각의 수.

    public enum STEP
    {

        NONE = -1,

        PLAY = 0,   // 퍼즐 게임중.
        CLEAR,      // 클리어 연출중.

        NUM,
    };

    public STEP step = STEP.NONE;
    public STEP next_step = STEP.NONE;

    private float step_timer = 0.0f;
    private float step_timer_prev = 0.0f;

    // -------------------------------------------------------- //

    // 모든 조각.
    private PieceControl[] all_pieces;

    // 생각 중인 조각. 앞부터 표시된 순서로 나열되어 있다.
    private PieceControl[] active_pieces;

    // 조각을 셔플하여 배치할 장소(범위).
    // unity 구조체 Bounds는 축으로 된 경계 상자를 나타남
    // 상자는 축에 대해 절대 회전하지 않으므로 중심 및 범위만 정의하거나 최소 및 최대 점을 정의 가능하다
    private Bounds shuffle_zone;

    // [degree] 퍼즐 전체를 회전하는 각도.
    private float pazzle_rotation = 37.0f;

    // 조각을 셔플하여 배치한다. 그리드의 셀 수(1변)
    // 한 변의 수??
    private int shuffle_grid_num = 1;

    // 『완성！』을 표시할 것인가?
    private bool is_disp_cleared = false;

    // -------------------------------------------------------- //

    void Start()
    {
        
        this.game_control = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();

        // 조각 오브젝트의 수를 확인한다.

        this.piece_num = 0;

        for (int i = 0; i < this.transform.childCount; i++)
        {  // 자식의 수만큼 반복

            GameObject piece = this.transform.GetChild(i).gameObject;
            //자식의 수만큼 퍼즐 조각에 넣어준다(하위 오브젝트 수만큼)

            if (!this.is_piece_object(piece))
            {
                continue;
            }

            this.piece_num++;
        }

        //

        this.all_pieces = new PieceControl[this.piece_num];
        this.active_pieces = new PieceControl[this.piece_num];

        // 각 조각에 PieceControl 컴포넌트（스크립트 "PieceControl.cs"）를
        // 추가한다.
        // 아래의 for문을 통해 자식 오브젝트(조각들)에게 PieceControl를 추가해줌(게임이 시작되면 조각들이 생성되도록)
        for (int i = 0, n = 0; i < this.transform.childCount; i++)
        {

            GameObject piece = this.transform.GetChild(i).gameObject;

            if (!this.is_piece_object(piece))
            {

                continue;
            }

            piece.AddComponent<PieceControl>();

            piece.GetComponent<PieceControl>().pazzle_control = this;

            this.all_pieces[n++] = piece.GetComponent<PieceControl>();
        }

        this.piece_finished_num = 0;

        // 조각의 그리기 순서를 정한다.
        //
        this.set_height_offset_to_pieces();

        // 베이스의 그리기 순서를 정한다.
        //
        for (int i = 0; i < this.transform.childCount; i++)
        {

            GameObject game_object = this.transform.GetChild(i).gameObject;

            if (this.is_piece_object(game_object))
            {

                continue;
            }

            game_object.GetComponent<Renderer>().material.renderQueue = this.GetDrawPriorityBase();
        }

        // 조각을 셔플하여 배치할 장소(범위)를 구한다. 
        //
        this.calc_shuffle_zone();


        this.is_disp_cleared = false;
         
    }

    void Update()
    {
        // ---------------------------------------------------------------- //

        this.step_timer_prev = this.step_timer;

        this.step_timer += Time.deltaTime;

        // ---------------------------------------------------------------- //
        // 상태 변화 점검.

        switch (this.step)
        {

            case STEP.NONE:
                {
                    this.next_step = STEP.PLAY;
                }
                break;

            case STEP.PLAY:
                {
                    // 조각이 전부 바른 위치에 놓이면 성공.
                    if (this.piece_finished_num >= this.piece_num)
                    {

                        this.next_step = STEP.CLEAR;
                    }
                }
                break;
        }

        // ---------------------------------------------------------------- //
        // 변화 시 초기화.

        if (this.next_step != STEP.NONE)
        {

            switch (this.next_step)
            {

                case STEP.PLAY:
                    {
                        // this.active_pieces = this.all_pieces 가 되면 배열도 복사되므로
                        // 각각 한 개씩 복사하도록 한다.
                        for (int i = 0; i < this.all_pieces.Length; i++)
                        {

                            this.active_pieces[i] = this.all_pieces[i];
                        }

                        this.piece_finished_num = 0;

                        this.shuffle_pieces();

                        foreach (PieceControl piece in this.active_pieces)
                        {

                            piece.Restart();
                        }

                        // 조각의 그리기 순서를 정한다.
                        //
                        this.set_height_offset_to_pieces();
                    }
                    break;

                case STEP.CLEAR:
                    {
                    }
                    break;
            }

            this.step = this.next_step;
            this.next_step = STEP.NONE;

            this.step_timer = 0.0f;
        }

        // ---------------------------------------------------------------- //
        // 실행 처리.

        switch (this.step)
        {

            case STEP.CLEAR:
                {
                    // 퍼즐 완성 시의 효과.

                    const float play_se_delay = 0.40f;

                    if (this.step_timer_prev < play_se_delay && play_se_delay <= this.step_timer)
                    {

                        this.game_control.playSe(GameControl.SE.COMPLETE);
                        this.is_disp_cleared = true;
                    }
                }
                break;
        }

        PazzleControl.DrawBounds(this.shuffle_zone);
    }

    // 『다시하기』버튼을 눌렀을 때. 
    public void beginRetryAction()
    {
        this.next_step = STEP.PLAY;
    }

    // 조각을 드래그 했을 때의 처리.
    public void PickPiece(PieceControl piece)
    {
        int i, j;

        // 클릭한 조각을 배열에 앞쪽에 이동시킨다.
        //
        // this.pieces[] 는 표시되는 순서대로 나열되어 있으므로, 앞쪽으로 조각을 가져가면
        // 가장 위에 표시된다.

        for (i = 0; i < this.active_pieces.Length; i++)
        {

            if (this.active_pieces[i] == null)
            {

                continue;
            }

            if (this.active_pieces[i].name == piece.name)
            {

                // 『클릭한 조각』보다 앞에 있는 조각은 한 개씩 뒤로 밀려난다.
                //
                for (j = i; j > 0; j--)
                {

                    this.active_pieces[j] = this.active_pieces[j - 1];
                }

                // 클릭한 조각을 앞쪽으로 가져온다.      
                this.active_pieces[0] = piece;

                break;
            }
        }

        this.set_height_offset_to_pieces();
    }

    // 조각이 정답 위치에 놓였을 때의 처리.
    public void FinishPiece(PieceControl piece)
    {
        int i, j;

        piece.GetComponent<Renderer>().material.renderQueue = this.GetDrawPriorityFinishedPiece();

        // 클릭한 조각을 배열에서 삭제한다. 

        for (i = 0; i < this.active_pieces.Length; i++)
        {

            if (this.active_pieces[i] == null)
            {
                continue;
            }

            if (this.active_pieces[i].name == piece.name)
            {

                // 『클릭한 조각』보다 뒤에 있는 조각을 한 개씩 앞으로 당겨진다.
                //
                for (j = i; j < this.active_pieces.Length - 1; j++)
                {
                    this.active_pieces[j] = this.active_pieces[j + 1];
                }

                // 배열 끝에 빈 공간이 생긴다.
                this.active_pieces[this.active_pieces.Length - 1] = null;

                // 『정답을 마친 조각』의 수를＋１한다.
                this.piece_finished_num++;

                break;
            }
        }

        this.set_height_offset_to_pieces();
    }

    // ---------------------------------------------------------------------------------------- //

    private static float SHUFFLE_ZONE_OFFSET_X = 0.0f;     //-5.0f
    private static float SHUFFLE_ZONE_OFFSET_Y = -1.3f;      //1.0f
    private static float SHUFFLE_ZONE_SCALE = 0.5f;         //1.1f

    // 조각을 셔플하여 배치할 장소(범위)를 구한다.
    private void calc_shuffle_zone()
    {
        Vector3 center;

        // 조각을 뿌리는 범위의 중심.

        center = Vector3.zero;

        /*
        foreach (PieceControl piece in this.all_pieces)
        {
            center += piece.finished_position;
        }
         * */

        
        center /= (float)this.all_pieces.Length;

        center.x += SHUFFLE_ZONE_OFFSET_X;
        center.z += SHUFFLE_ZONE_OFFSET_Y;

       
        // 조각을 배치할 그리드의 셀 수

        this.shuffle_grid_num = Mathf.CeilToInt(Mathf.Sqrt((float)this.all_pieces.Length));

        // 조각의 바운딩 박스 중에서 가장 큰 것 = 셀 1개의 크기

        Bounds piece_bounds_max = new Bounds(Vector3.zero, Vector3.zero);

        
        foreach (PieceControl piece in this.all_pieces)
        {

            Bounds bounds = piece.GetBounds(Vector3.zero);

            piece_bounds_max.Encapsulate(bounds);

        }
        
        // Bounds.size 바운딩 박스의 전체크기
        piece_bounds_max.size *= SHUFFLE_ZONE_SCALE;

        this.shuffle_zone = new Bounds(center, piece_bounds_max.size * this.shuffle_grid_num);

    }

    // 조각의 위치를 셔플한다.   
    private void shuffle_pieces()
    {
#if true
        // 조각을 그리드에 순서대로 나열한다.

        int[] piece_index = new int[this.shuffle_grid_num * this.shuffle_grid_num];

        for (int i = 0; i < piece_index.Length; i++)
        {

            if (i < this.all_pieces.Length)
            {

                piece_index[i] = i;

            }
            else
            {

                piece_index[i] = -1;
            }

        }

        // 임의로 조각 두 개를 선택하여 위치를 바꾼다.

        for (int i = 0; i < piece_index.Length - 1; i++)
        { // 조각의 수는 8개 이지만 그리드의 수는 9개이므로 -1만큼 해줘야함 

            int j = Random.Range(i + 1, piece_index.Length);

            int temp = piece_index[j];

            piece_index[j] = piece_index[i];

            piece_index[i] = temp;
        }
        
        // 네모칸의 인덱스를 실제 좌표로 변환하여 배치한다.
        Vector3 pitch;      //pitch = 네모칸의 사이즈

        pitch = this.shuffle_zone.size / (float)this.shuffle_grid_num;      // 네모칸의 사이즈 = 조각의 범위 및 사이즈 / 조각의 수 

        for (int i = 0; i < piece_index.Length; i++)
        {

            if (piece_index[i] < 0)
            {

                continue;
            }

            PieceControl piece = this.all_pieces[piece_index[i]];

            Vector3 position = piece.finished_position;

            int ix = i % this.shuffle_grid_num;
            int iz = i / this.shuffle_grid_num;

            position.x = ix * pitch.x;
            position.z = iz * pitch.z;

            // 좌표의 계산식이 어떻게 구해지는지??
            position.x += this.shuffle_zone.center.x - pitch.x * (this.shuffle_grid_num / 2.0f - 0.5f);
            position.z += this.shuffle_zone.center.z - pitch.z * (this.shuffle_grid_num / 2.0f - 0.5f);

            position.y = piece.finished_position.y;

            piece.start_position = position;
        }

        // 조금씩 (그리드의 셀 내에서) 무작위로 위치를 바꾼다.

        Vector3 offset_cycle = pitch / 2.0f; //2.0
        Vector3 offset_add = pitch / 5.0f;  //5.0
        Vector3 offset = Vector3.zero;

        for (int i = 0; i < piece_index.Length; i++)
        {

            if (piece_index[i] < 0)
            {

                continue;
            }

            PieceControl piece = this.all_pieces[piece_index[i]];

            Vector3 position = piece.start_position;

            position.x += offset.x;
            position.z += offset.z;

            piece.start_position = position;

            //


            offset.x += offset_add.x;

            if (offset.x > offset_cycle.x / 2.0f)
            {

                offset.x -= offset_cycle.x;
            }

            offset.z += offset_add.z;

            if (offset.z > offset_cycle.z / 2.0f)
            {

                offset.z -= offset_cycle.z;
            }
        }

        // 전부 회전시킨다.

        foreach (PieceControl piece in this.all_pieces)
        {

            Vector3 position = piece.start_position;

            position -= this.shuffle_zone.center;

            position = Quaternion.AngleAxis(this.pazzle_rotation, Vector3.up) * position;

            position += this.shuffle_zone.center;

            piece.start_position = position;
        }

        this.pazzle_rotation += 90;

#else
		// 단순히 난수로 좌표를 정하는 경우.
		foreach(PieceControl piece in this.all_pieces) {

			Vector3	position;

			Bounds	piece_bounds = piece.GetBounds(Vector3.zero);

			position.x = Random.Range(this.shuffle_zone.min.x - piece_bounds.min.x, this.shuffle_zone.max.x - piece_bounds.max.x);
			position.z = Random.Range(this.shuffle_zone.min.z - piece_bounds.min.z, this.shuffle_zone.max.z - piece_bounds.max.z);

			position.y = piece.finished_position.y;

			piece.start_position = position;
		}
#endif
    }

    // 조각 GameObject?
    private bool is_piece_object(GameObject game_object)
    {
        bool is_piece = false;

        do
        {

            // 이름에 "base" 가 포함된 것은 바닥 오브젝트.
            if (game_object.name.Contains("Box001"))
            {

                continue;
            }

            //

            is_piece = true;

        } while (false);

        return (is_piece);
    }


    // ---------------------------------------------------------------------------------------- //

    // 모든 조각에 높이 오프셋을 설정한다.         
    private void set_height_offset_to_pieces()
    {
        float offset = 0.01f;

        int n = 0;

        foreach (PieceControl piece in this.active_pieces)
        {

            if (piece == null)
            {

                continue;
            }

            // 조각의 배치 순서를 정한다.
            // pieces  이전에 놓인 조각보다 나중에 놓일 조각이 우선 배치되도록 순서를 정한다.
            //
            piece.GetComponent<Renderer>().material.renderQueue = this.GetDrawPriorityPiece(n);

            // 클릭했을 때 나중에 놓일 조각의 OnMouseDown() 를 불러올 수 있도록
            // Y높이 오프셋도 지정해둔다.
            // （높이 오프셋을 설정하지 않으면 아래의 조각이 클릭될 수 있기 때문이다.)


            offset -= 0.01f / this.piece_num;

            //piece.SetHeightOffset(offset);

            //

            n++;
        }
    }

    // 우선 배치되도록 정한다.（바닥）.
    private int GetDrawPriorityBase()
    {
        return (0);
    }

    // 우선 배치되도록 정한다.（바른 위치에 놓인 조각）.
    private int GetDrawPriorityFinishedPiece()
    {
        int priority;

        priority = this.GetDrawPriorityBase() + 1;

        return (priority);
    }

    // 우선 배치되도록 정한다.（『다시하기』버튼）.
    public int GetDrawPriorityRetryButton()
    {
        int priority;

        priority = this.GetDrawPriorityFinishedPiece() + 1;

        return (priority);
    }

    // 우선 배치되도록 정한다.（바른 위치에 놓인 조각）.
    private int GetDrawPriorityPiece(int priority_in_pieces)
    {
        int priority;

        // 『다시하기』버튼의 분량을 염두하여 한 개 더 더해두다.
        priority = this.GetDrawPriorityRetryButton() + 1;

        // priority_in_pieces 는０번이 가장 위에 있기 때문에 renderQueue는 값이 작은 쪽부터 배치된다.
        priority += this.piece_num - 1 - priority_in_pieces;

        return (priority);
    }

    // ---------------------------------------------------------------------------------------- //

    // 퍼즐이 완성되었는가?
    public bool isCleared()
    {
        return (this.step == STEP.CLEAR);
    }

    // 『완성！』 표시
    public bool isDispCleard()
    {
        return (this.is_disp_cleared);
    }

    // ---------------------------------------------------------------------------------------- //


    // 하얀색 박스 칸이 만들어지는 메소드?
    public static void DrawBounds(Bounds bounds)
    {
        Vector3[] square = new Vector3[4];

        square[0] = new Vector3(bounds.min.x, 0.0f, bounds.min.z);
        square[1] = new Vector3(bounds.max.x, 0.0f, bounds.min.z);
        square[2] = new Vector3(bounds.max.x, 0.0f, bounds.max.z);
        square[3] = new Vector3(bounds.min.x, 0.0f, bounds.max.z);

        for (int i = 0; i < 4; i++)
        {
            // DrawLine( Vector3의 시작, Vector3의 끝, 컬러색상) : 지정된 시작점과 끝점 사이에 선을 그림
            Debug.DrawLine(square[i], square[(i + 1) % 4], Color.white, 0.0f, false);
        }

    }
}
