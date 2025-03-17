using Unity.Netcode;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ThrowSystem : NetworkBehaviour
{
    [SerializeField] NetworkManager networkManager;
    [SerializeField] GameObject bomb;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject hand;
    Playermovement playermovement;
    [SerializeField]  SpriteRenderer handSpriteRenderer;

    private Vector3 defaultHandPostion;
    private Vector3 mouse_pos;
    private Vector3 object_pos;
    private float angle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultHandPostion = hand.transform.localPosition;
        hand = transform.Find("hand").gameObject;
        playermovement = transform.GetComponent<Playermovement>();
        handSpriteRenderer = hand.transform.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
            handSpriteRenderer.flipX = playermovement.isFlipped.Value;

            mouse_pos = Input.mousePosition;
            mouse_pos.z = -20;
            object_pos = Camera.main.WorldToScreenPoint(hand.transform.position);
            mouse_pos.x = mouse_pos.x - object_pos.x;
            mouse_pos.y = mouse_pos.y - object_pos.y;
            angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;

            if (playermovement.isFlipped.Value)
            {
                hand.transform.rotation = Quaternion.Euler(0, 0, angle + 180);
                hand.transform.localPosition = new Vector3(-defaultHandPostion.x, defaultHandPostion.y, defaultHandPostion.z);
            }
            else
            {
                hand.transform.rotation = Quaternion.Euler(0, 0, angle);
                hand.transform.localPosition = defaultHandPostion;
            }
            
    }
}
