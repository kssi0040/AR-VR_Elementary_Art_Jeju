using UnityEngine;
using System.Collections;

// MeshCollider 가 필요.
[RequireComponent(typeof(MeshCollider))]
public class PieceControl : MonoBehaviour
{

    // 게임 카메라.
    private GameObject game_camera;        // 오브젝트 큰판을 씌움


    public PazzleControl pazzle_control = null;  //puzzleControl(script파일)객체

    public GameControl game_control = null;    //GameControl(script파일)객체

    // -------------------------------------------------------- //

    // 드래그 할 때 마우스 커서 위치를 
    // 처음에 집었던 장소가 되도록 조각을 이동한다.
    // （false 가 되면 마우스 커서 위치=조각의 중심이 된다）.
    // false로 바꿀 경우 조각의 끝부분을 잡아도 조각의 중심이 마우스 커서 위치로 이동됨(자연스러운 이동이 아닌 끌려오는느낌?)
    // 조각의 중심을 잡지 않고 마우스 좌표와 오프셋만큼의 거리를 측정하기 위해 true로 기본 베이스를 설정?
    private const bool IS_ENABLE_GRAB_OFFSET = true;


    private const float HEIGHT_OFFSET_BASE = 0.1f;      //오프셋의 기본 베이스 거리?

    private const float SNAP_SPEED_MIN = 0.01f * 60.0f;   //이동시최소속도값
    private const float SNAP_SPEED_MAX = 0.8f * 60.0f;    //이동시최고속도값

    // -------------------------------------------------------- //

    // 마우스로 잡은 위치.
    private Vector3 grab_offset = Vector3.zero;
    //unity에서 제공하는 vector3의 구조체의 shortcut에 의하면 Vector3.zero = Vector3(0,0,0)임

    // 드래그 중?
    // 처음 드래그 상태를 false로 설정
    // 마우스 버튼을 눌렀을 때 OnMouseDown() 메소드로 인해 true로 설정
    private bool is_now_dragging = false;

    // 초기 위치＝정답의 위치
    public Vector3 finished_position;

    // 시작 위치（무작위로 뿌려진 상태）
    // height_offset과 roll의 초기화
    public Vector3 start_position;

    public float height_offset = 0.0f;

    public float roll = 0.0f;

    // 스냅처리되는 거리.
    //（스냅＝정답 위치에 가까운 곳에 놓일 때 정답 위치로 끌려오는 처리）.
    static float SNAP_DISTANCE = 0.5f;

    // 조각의 상태.
    // enum 키워드는 열거자 목록이라고 하는 명명된 상수 집합으로 구성된 고유 형식
    // 첫부분 수를 정의 해주면 다음 아래 변수에는 자동적으로 한단계씩 숫자가 증가한다.
    // ex) NONE에 -1이 들어가있으니 IDLE은 0이고 DRAGING에는 자동적으로 1이 들어가있다. 상수로만 되어있으면 구분을 못하므로 enum으로 고유명지정
    enum STEP
    {

        NONE = -1,

        IDLE = 0,       // 오답.
        DRAGING,        // 드래그 중.
        FINISHED,       // 정답 위치에 놓여있다（더 이상 드래그 불가）.
        RESTART,        // 처음부터.
        SNAPPING,       // 스냅 중.

        NUM,
    };

    // 조각의 현재 상태.
    private STEP step = STEP.NONE;

    private STEP next_step = STEP.NONE;

    // 스냅 시의 목표위치.
    private Vector3 snap_target;

    // 스냅 후 실행되는 상태.
    private STEP next_step_snap;

    // -------------------------------------------------------- //

    void Awake()
    {
        // 초기 위치＝정답 위치를 기억해둔다.
        this.finished_position = this.transform.position;

        // (만일 다시) 초기화하면 
        // 나중에 셔플한 위치에 다시 놓인다.
        this.start_position = this.finished_position;

    }

    void Start()
    {
        // 카메라 인스턴스를 찾아둔다.  
        this.game_camera = GameObject.FindGameObjectWithTag("MainCamera");

        // Start() 는　PazzleControl.Start() 에서의 처리가 모두 종료된 후에 불러오기 때문에
        // position 이 이미 이동된 것으로 처리된다.
        // this.initial_position = this.transform.position;

        //GameControl(script파일)객체에 Tag가 GameController인 컴포넌트를 받아온다.
        this.game_control = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();

    }
    void Update()
    {
        Color color = Color.white;

        // 상태 변화
        // enum,상수집합 STEP 의 객체 step
        // 조각의 현재상태를 나타내는 스위치문
        switch (this.step)
        {

            case STEP.NONE:     //초기에 NONE = -1 이 들어가있다
                {
                    this.next_step = STEP.RESTART;  //처음시작 enum상수에 RESTART는 3할당
                }
                break;

            case STEP.IDLE:    //0의 경우 (정답이 아닌 경우)
                {
                    if (this.is_now_dragging)
                    {  //is_now_dragging이 false로 되어있으며 true로 드래깅이 활성화되면 if문실행

                        // 드래그 시작.
                        // 정답이 아닌 경우 이므로 드래그가 가능한 상태로 만들도록 함?
                        this.next_step = STEP.DRAGING;  //현재상태에 드래그 시작 부여
                    }
                }
                break;

            case STEP.DRAGING:  //1의 경우
                {
                    if (this.is_in_snap_range())
                    {
                        // 정답의 유무를 확인하는 메소드(평소때는 ret가 false로 되어있다. 정답일 경우 true)
                        // 스냅할 수 있는 곳（정답 위치에 가까이）에 있을 때.

                        bool is_snap = false;
                        // 버튼을 떼었을 때 OnMouseUp() 메소드로 인해 is_now_dragging이 false로 바뀜
                        // 버튼을 떼었을 때에 스냅한다.
                        if (!this.is_now_dragging)
                        {     // 정답 근처에서 드래그를 놓았을 때(드래그를 놓았을 때) 

                            is_snap = true;             //정답 위치에 퍼즐이 들어갔을 때 true상태로 변환
                        }

                        if (is_snap)
                        {                   //정답 위치에 들어가면 

                            // 이 조각은 이미 종료됨.

                            this.next_step = STEP.SNAPPING;
                            this.snap_target = this.finished_position;
                            this.next_step_snap = STEP.FINISHED;

                            this.game_control.playSe(GameControl.SE.ATTACH);    //정답인 경우 오디오
                        }

                    }
                    else
                    {

                        // 스냅하지 않는 곳（정답 위치에서 멀리 떨어진）에 있을 때.

                        if (!this.is_now_dragging)
                        {

                            // 버튼을 떼었다.

                            this.next_step = STEP.IDLE;     //현재 위치가 정답이 아님

                            this.game_control.playSe(GameControl.SE.RELEASE);   //정답이 아닌 경우 오디오
                        }
                    }
                }
                break;

            case STEP.SNAPPING:
                {
                    // 목표 위치에 도착하였을 때 종료.

                    if ((this.transform.position - this.snap_target).magnitude < 0.0001f)
                    {      //목표위치와 가까울 때의 if문
                           // Vector3.magnitude : 오브젝트간에 transfrom.position의 거리를 체크하고 싶을 때 사용

                        this.next_step = this.next_step_snap;
                    }
                }
                break;
        }

        // 상태 변화시의 초기화 처리.

        while (this.next_step != STEP.NONE)
        {        //현재 상태가 종료되지 않을 때 까지?

            this.step = this.next_step;

            this.next_step = STEP.NONE;

            switch (this.step)
            {

                case STEP.IDLE:
                    {
                        this.SetHeightOffset(this.height_offset);   //높이오프셋메소드 : SetHeightOffset()
                    }
                    break;

                case STEP.RESTART:
                    {
                        this.transform.position = this.start_position;

                        this.SetHeightOffset(this.height_offset);

                        this.next_step = STEP.IDLE;
                    }
                    break;

                case STEP.DRAGING:
                    {
                        this.begin_dragging();

                        // 퍼즐 관리 클래스에 드래그가 시작되었음을 알린다.
                        this.pazzle_control.PickPiece(this);

                        this.game_control.playSe(GameControl.SE.GRAB);
                    }
                    break;

                case STEP.FINISHED:
                    {
                        // 정답 위치에 가까운 곳에 드래그되었다.

                        // 정답 위치에 스냅한다.
                        this.transform.position = this.finished_position;

                        // 퍼즐 관리 클래스에 조각이 정답 위치에 놓였음을 알린다.
                        this.pazzle_control.FinishPiece(this);

                    }
                    break;
            }
        }

        // 각 상태 실행 처리.

        this.transform.localScale = Vector3.one;    // Vector3(1.1.1)으로 localScale(크기) 설정

        switch (this.step)
        {

            case STEP.DRAGING:
                {
                    this.do_dragging();

                    // 스냅 범위（정답 위치에 매우 가까이）에 들어가면 색을 밝게 한다.
                    if (this.is_in_snap_range())
                    {
                        //color = Color.cyan;
                        color = new Color32(141, 252, 240, 255);
                        color *= 1.5f;
                    }

                    this.transform.localScale = Vector3.one * 1.1f;   //색을 밝게하는 범위?
                }
                break;

            case STEP.SNAPPING:
                {
                    // 목표위치를 향해 이동한다.

                    Vector3 next_position, distance, move;

                    // 속도감이 느껴지는 움직임이 되도록 한다.

                    distance = this.snap_target - this.transform.position;

                    // 다음 장소＝현재 장소와 목표위치의 중간지점.
                    // 거리 연산 식의 기준이 무엇인지??
                    distance *= 0.25f * (60.0f * Time.deltaTime);       // 움직이는 속도

                    next_position = this.transform.position + distance;

                    move = next_position - this.transform.position;

                    float snap_speed_min = PieceControl.SNAP_SPEED_MIN * Time.deltaTime;
                    float snap_speed_max = PieceControl.SNAP_SPEED_MAX * Time.deltaTime;

                    if (move.magnitude < snap_speed_min)
                    {

                        // 이동량이 일정 이하가 되면 종료.
                        // 목표위치로 이동시킨다. 
                        // 종료판정은 상태변화 점검에서 실행되므로 여기에서는 위치 조정만 실행한다.

                        this.transform.position = this.snap_target;

                    }
                    else
                    {

                        // 이동 속도가 너무 빠르면 조정한다.
                        if (move.magnitude > snap_speed_max)
                        {

                            move *= snap_speed_max / move.magnitude;

                            next_position = this.transform.position + move;
                        }

                        this.transform.position = next_position;
                    }
                }
                break;
        }

        this.GetComponent<Renderer>().material.color = color;

        // 바운딩 박스를 표시한다.
        //PazzleControl.DrawBounds(this.GetBounds(this.transform.position));

        //
    }

    // 드래그 시작 처리
    private void begin_dragging()
    {
        do
        {

            // 커서 좌표를 3D공간의 월드 좌표로 변환한다.         

            Vector3 world_position;

            if (!this.unproject_mouse_position(out world_position, Input.mousePosition))
            {

                break;
            }

            if (PieceControl.IS_ENABLE_GRAB_OFFSET)
            {

                // 오프셋（클릭한 위치가 조각의 중심에서 어느정도 떨어져 있는가）을 구한다. 
                this.grab_offset = this.transform.position - world_position;
            }

        } while (false);
    }

    // 드래그 중의 처리.
    private void do_dragging()
    {
        do
        {

            // 커서 좌표를 3D공간의 월드 좌표로 변환한다.   
            Vector3 world_position;

            if (!this.unproject_mouse_position(out world_position, Input.mousePosition))
            {
                break;
            }

            // 커서 좌표（３D）에 오프셋을 더해 조각의 중심좌표를 계산한다.
            this.transform.position = world_position + this.grab_offset;

        } while (false);
    }

    // 조각의 바운딩박스를 정한다.    
    public Bounds GetBounds(Vector3 center)
    {
        // Mesh 는 Component 가 아니기 때문에 GetComponent<Mesh>() 를 할 수 없다.

        Bounds bounds = this.GetComponent<MeshFilter>().mesh.bounds;

        // 중심위치를 설정한다.
        // （min、max 도 자동적으로 재계산된다）.
        bounds.center = center;

        return (bounds);
    }

    public void Restart()
    {
        this.next_step = STEP.RESTART;
    }

    // 마우스 버튼을 눌렀을 때.
    void OnMouseDown()
    {
        this.is_now_dragging = true;
    }

    // 마우스 버튼을 떼었을 때.
    void OnMouseUp()
    {
        this.is_now_dragging = false;
    }

    // 높이 오프셋을 정한다. 
    public void SetHeightOffset(float height_offset)
    {
        Vector3 position = this.transform.localPosition;

        this.height_offset = 0.0f;

        // 정답 위치에 놓였을 때에만 유효함.
        if (this.step != STEP.FINISHED || this.next_step != STEP.FINISHED)
        {

            this.height_offset = height_offset;

            position.y = this.finished_position.y + PieceControl.HEIGHT_OFFSET_BASE;
            position.y += this.height_offset;

            this.transform.localPosition = new Vector3(0, position.y, position.z);
        }
    }

    // 마우스 위치를 3D공간의 월드 좌표로 변환한다.         
    //
    // ・마우스 커서와 카메라 위치를 잇는 직선.
    // ・ 지면에 닿았는지를 판정하는 평면.
    //　↑이 두 조건이 교차하는 곳을 구한다.
    //
    public bool unproject_mouse_position(out Vector3 world_position, Vector3 mouse_position)
    {
        bool ret;
        float depth;

        // 조각의 중심을 통과하는 수평（법선이 Y축.XZ평면）인 면.
        Plane plane = new Plane(Vector3.up, new Vector3(0.0f, this.transform.position.y, 0.0f));

        // 카메라 위치와 마우스 커서의 위치를 잇는 직선.
        // ScreenPointToRay 메소드 : Screen 상에서의 좌표를 World space 상에서의 좌표로 변환하여 시작점으로 설정
        // 하고 direction (Ray가 시작되는 지점에서 쏘여지는 방향 설정) 을 카메라가 비추는 방향으로 설정 된 Ray 객체를 반환
        Ray ray = this.game_camera.GetComponent<Camera>().ScreenPointToRay(mouse_position);

        // 위의 두 조건이 교차하는 곳을 구한다.

        if (plane.Raycast(ray, out depth))
        {

            world_position = ray.origin + ray.direction * depth;

            ret = true;

        }
        else
        {

            world_position = Vector3.zero;

            ret = false;
        }

        return (ret);
    }

    // 스냅 가능한 위치？（정답 위치에 가까운 지점)
    private bool is_in_snap_range()
    {
        bool ret = false;

        if (Vector3.Distance(this.transform.position, this.finished_position) < PieceControl.SNAP_DISTANCE)
        {
            // 놓는 위치가 정답과 가까운 경우 ret 를 true

            ret = true;
        }

        return (ret);
    }
}
