using UnityEngine;

public class BallScript : MonoBehaviour
{
    char letter;
    Transform target;
    Transform supar;
    bool reflected = false;

    public void Init(char c, Transform player, Transform suparPos)
    {
        letter = c;
        target = player;
        supar = suparPos;

        // UI•\Ž¦“™‚ª‚ ‚é‚È‚ç‚±‚±‚Å
        // GetComponentInChildren<Text>().text = letter.ToString();
    }

    void Update()
    {
        if (target == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            5f * Time.deltaTime
        );
    }

    public void Reflect()
    {
        if (reflected) return;

        reflected = true;
        target = supar;
    }
}
