using Patterns.State.Interfaces;
using UnityEngine;

namespace Patterns.State.States
{
    public class ChasingPlayer : AZombieState
    {
        private Transform playerTransform;
        private Transform currentTransform;

        private float rotationSpeed;
        private float chaseSpeed;
        
        public ChasingPlayer(IZombie zombie) : base(zombie)
        {
        }

        public override void Enter()
        {
            currentTransform = zombie.GetGameObject().transform;
            playerTransform = zombie.PlayerAtSight().transform;
            rotationSpeed = zombie.GetRotateSpeed();
            chaseSpeed = zombie.GetChaseSpeed();
            Debug.Log($"Zombie {zombie.GetGameObject().name} started chasing player");
        }

        public override void Exit()
        {
            Debug.Log($"Zombie {zombie.GetGameObject().name} ended chasing player");
        }

        public override void Update()
        {
        }

        public override void FixedUpdate()
        {
            if (zombie.PlayerAtSight())
            {
                zombie.MoveTo(playerTransform, chaseSpeed, rotationSpeed);
            }
            else
            {
                zombie.SetState(new WalkingToWaypoint(zombie));
            }
        }
    }
}