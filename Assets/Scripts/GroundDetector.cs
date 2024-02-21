using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    bool _isGrounded = false;

    public bool IsGrounded { get => _isGrounded; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        checkGroundCollider(other, true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        checkGroundCollider(other, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        checkGroundCollider(other, false);
    }

    private void checkGroundCollider(Collider2D other, bool grounded)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = grounded;
        }
    }
}
