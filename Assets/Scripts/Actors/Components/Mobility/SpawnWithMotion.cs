using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpawnWithMotion : MonoBehaviour
    {
        private Rigidbody2D rb;
        [SerializeField] private Momentum momentum = Momentum.OVERRIDE;
        [SerializeField] private Vector2 parentVelocity = new Vector2();

        [SerializeField] private AngleRange angleRange;
        [SerializeField] private VelocityRange velocityRange;
        [SerializeField] private RotationRange rotationRange;

        public void SetMomentum(Momentum momentum) => this.momentum = momentum;
        public void RecordParentVelocity(Vector2 parentVelocity) => this.parentVelocity = parentVelocity;
        public void SetAngleRange(AngleRange angleRange) => this.angleRange = angleRange;
        public void SetVelocityRange(VelocityRange velocityRange) => this.velocityRange = velocityRange;
        public void SetRotationRange(RotationRange rotationRange) => this.rotationRange = rotationRange;

        public void ResetVelocity()
        {
            int rotation = rotationRange.GetRotationWithinRange();
            rb.AddTorque(rotation);

            float velocity = velocityRange.GetVelocityWithinRange();
            if (momentum == Momentum.OVERRIDE)
            {
                Vector2 direction = angleRange.GetNormalizedVectorWithinRange(transform.position);
                rb.AddForce(direction * velocity, ForceMode2D.Impulse);
            }
            else if (momentum == Momentum.RETAIN)
                rb.AddForce(parentVelocity * rb.mass, ForceMode2D.Impulse);
            else if (momentum == Momentum.AVERAGE)
            {
                Vector2 innateDirection = angleRange.GetNormalizedVectorWithinRange(transform.position);
                Vector2 direction = (innateDirection + parentVelocity.normalized) * velocity;
                rb.AddForce(direction, ForceMode2D.Impulse);
            }
        }

        private void Start() 
        { 
            ResetVelocity();
            Actor actor = GetComponent<Actor>();
            if (actor)
                actor.ActivateInPoolEvent.AddListener(ResetVelocity);
        }

        private void Awake() { rb = GetComponent<Rigidbody2D>(); }
    }
}