using UnityEngine;

public enum JumpType
{
    NormalGrounded,
    Coyote,
}

public class JumpIndicator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    public void Place(JumpType type)
    {
        if(_sprite == null) { return; }

        switch (type)
        {
            case JumpType.NormalGrounded:
                _sprite.color = Color.gray;

                break;
            case JumpType.Coyote:
                break;
            default:
                break;
        }
    }
}