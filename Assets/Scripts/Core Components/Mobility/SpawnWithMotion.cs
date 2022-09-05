using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceMage.Actors
{
    /// <summary>
    /// Impart motion to this object when it is spawned.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpawnWithMotion : MonoBehaviour
    {
        private Rigidbody2D rb;                                                                 // Assigned in Awake().

        private Vector2 parentVelocity = Vector2.zero;                                          // Assigned by method call. Optional.

        [SerializeField] private HandleMomentum handleMomentum = HandleMomentum.OVERRIDE;       // Editor configurable. How parent velocity is handled.
        [SerializeField] private AngleRange angleRange;                                         // Editor configurable.
        [SerializeField] private VelocityRange velocityRange;                                   // Editor configurable.
        [SerializeField] private RotationRange rotationRange;                                   // Editor configurable.

        public HandleMomentum HandleMomentum => HandleMomentum;
        public AngleRange AngleRange => angleRange;
        public VelocityRange VelocityRange => velocityRange;
        public RotationRange RotationRange => rotationRange;

        public void SetHandleMomentum(HandleMomentum handleMomentum) => this.handleMomentum = handleMomentum;
        public void RecordParentVelocity(Vector2 parentVelocity) => this.parentVelocity = parentVelocity;
        public void SetAngleRange(AngleRange angleRange) => this.angleRange = angleRange;
        public void SetVelocityRange(VelocityRange velocityRange) => this.velocityRange = velocityRange;
        public void SetRotationRange(RotationRange rotationRange) => this.rotationRange = rotationRange;

        /// <summary>
        /// Imparts motion to this object.
        /// </summary>
        public void ImpartMotion()
        {
            ImpartVelocity(angleRange.GetNormalizedVectorWithinRange(transform.position), velocityRange.GetVelocityWithinRange());
            ImpartRotation();
        }

        /// <summary>
        /// Imparts motion to this object, taking parent velocity into account.
        /// </summary>
        /// <param name="parentVelocity">The velocity of the parent object.</param>
        public void ImpartMotion(Vector2 parentVelocity)
        {
            if (handleMomentum == HandleMomentum.OVERRIDE)
                ImpartVelocity(angleRange.GetNormalizedVectorWithinRange(transform.position), velocityRange.GetVelocityWithinRange());
            else if (handleMomentum == HandleMomentum.RETAIN)
                ImpartVelocity(parentVelocity, rb.mass); 
            else if (handleMomentum == HandleMomentum.AVERAGE)
                ImpartVelocity(angleRange.GetNormalizedVectorWithinRange(transform.position) * parentVelocity.normalized, velocityRange.GetVelocityWithinRange());

            ImpartRotation();
        }

        private void ImpartVelocity(Vector2 direction, float velocity, ForceMode2D forceMode = ForceMode2D.Impulse) => rb.AddForce(direction * velocity, forceMode);

        private void ImpartRotation()
        {
            int rotation = rotationRange.GetRotationWithinRange();
            rb.AddTorque(rotation);
        }

        private void Start() 
        { 
            ImpartMotion();
            Actor actor = GetComponent<Actor>();
            if (actor)
                actor.ActivateInPoolEvent.AddListener(ImpartMotion);
        }

        private void Awake() => rb = GetComponent<Rigidbody2D>();
    }
}