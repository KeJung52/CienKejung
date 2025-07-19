using UnityEngine;

public class Players : MonoBehaviour
{
    [Header("Settings")]
    public float speed;
    public float maxSpeed;

    public float jumpPower;

    bool isOnIce = false; 

    bool isGrounded = true;

    [Header("References")]
    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    public Animator anim;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame

  
    void Update()
    {
        if(isOnIce == false)
        {
            //점프
            if(Input.GetButtonDown("Jump") && isGrounded) //방향 키 눌렸을 때
            {
                isGrounded = false;
                rigid.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse); // 힘 추가
             
            }


            // 좌우 움직임
            float h = Input.GetAxisRaw("Horizontal"); //키보드 방향 저장
            rigid.AddForce(Vector2.right*h*speed, ForceMode2D.Impulse); // 힘 추가

        
            if(rigid.linearVelocity.x > maxSpeed) ///힘이 너무 커지지않도록
            {
                rigid.linearVelocity = new Vector2(maxSpeed, rigid.linearVelocity.y);
            }

            if(rigid.linearVelocity.x < maxSpeed*(-1))
            {
                rigid.linearVelocity = new Vector2(maxSpeed*(-1), rigid.linearVelocity.y);
            }


            if(Input.GetButtonUp("Horizontal")) //좌우 키 땠을 때 속도 줄이기
            {   
            
                rigid.linearVelocity = new Vector2(rigid.linearVelocity.normalized.x*0.5f, rigid.linearVelocity.y);
            }
            

            //방향 전환
            if(Input.GetButtonDown("Horizontal")) //방향 키 눌렸을 때
            {
                spriteRenderer.flipX = (Input.GetAxisRaw("Horizontal") == -1);
            }
            
            // 애니메이션 변화 (걷기, 정지)
            if(Mathf.Abs(rigid.linearVelocity.x) < 0.5)
            {
                anim.SetBool("isWalking",false);
            }
            else
            {
                anim.SetBool("isWalking",true);
            }

        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ice")
        {
            isOnIce = true;
            isGrounded = true;
            rigid.linearDamping = 0;
        }
        else
        {
            isOnIce =false;
            isGrounded = true;
            rigid.linearDamping = 1;
        }
    }

}
