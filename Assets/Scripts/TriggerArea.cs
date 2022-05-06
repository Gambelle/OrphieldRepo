using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    private static readonly int TRIG1_PROPERTY = Animator.StringToHash("Trigger1");
    private static readonly int TRIG2_PROPERTY = Animator.StringToHash("DeadMouse");
    [Header("Relations")]
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private Animator animator1 = null;
    [SerializeField]
    private Animator animator2 = null;
    bool isTrigger2 = false;

    public GameObject player;
    
    
    

    public GameObject obj;
    public Rigidbody2D rb;
    public Animator fallingShelves;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("/object") != null)
        {
            obj = GameObject.Find("/object");
            rb = obj.GetComponent<Rigidbody2D>();
        }
        Debug.Log("Object started");
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("done")&& !isTrigger2)
        {
            animator.enabled = false;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            isTrigger2 = true;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        if (animator != null)
        {
            animator.enabled = true;
            animator.SetBool(TRIG1_PROPERTY, true);
        }
        if (animator2 != null)
        {
            animator2.SetBool(TRIG1_PROPERTY, true);
        }
        if (animator1 != null)
        {
            animator1.SetBool(TRIG2_PROPERTY, true);
        }
        Debug.Log("Object Entered the trigger");
       
        
    }
}
