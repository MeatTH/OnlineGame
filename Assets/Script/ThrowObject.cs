using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public GameObject projectilePrefab; // ����红ͧ�ѵ�ط���ͧ��û�
    public Transform throwPoint; // �ش��������¢ͧ (�Դ��������Ф�)
    public float forceMultiplier = 10f; // ��Ҥ����ç�ͧ��û�

    private Vector2 startPoint; // �ش������鹵͹�ҡ
    private Vector2 endPoint; // �ش�����
    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // �����������������ҡ
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) && isDragging) // ���������������ԧ
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Throw();
            isDragging = false;
        }
    }

    void Throw()
    {
        Vector2 throwDirection = (startPoint - endPoint).normalized; // ��ȷҧ����ԧ (��͹�ҡ�ҡ)
        float throwPower = (startPoint - endPoint).magnitude * forceMultiplier; // �ӹǳ�ç�ԧ

        GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity); // ���ҧ�ѵ��
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>(); // �֧ Rigidbody2D �ͧ�ѵ��

        if (rb != null)
        {
            rb.AddForce(throwDirection * throwPower, ForceMode2D.Impulse); // �ԧ�͡�
        }
    }
}
