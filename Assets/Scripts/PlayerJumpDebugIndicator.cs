using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerJumpDebugIndicator : MonoBehaviour
{
    [SerializeField] private JumpIndicator _indicatorPrefab;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();

        _player.OnJumpKeyPressed += OnPlayerJump;
    }

    private void OnPlayerJump()
    {
        var newInd = Instantiate(_indicatorPrefab, transform.position, Quaternion.identity);
        var jumpType = JumpType.NormalGrounded;

        if(!_player.OnGround)
        {
            jumpType = JumpType.Coyote;
        }

        newInd.Place(jumpType);

        Destroy(newInd.gameObject, 2f);
    }

    private void OnDisable()
    {
        _player.OnJumpKeyPressed -= OnPlayerJump;
    }

    private void OnDestroy()
    {
        _player.OnJumpKeyPressed -= OnPlayerJump;
    }
}
