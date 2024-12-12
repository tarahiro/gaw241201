using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace gaw241201
{
    public class TimerArgs
    {
        public float Time { get; set; }
        public CancellationToken CancellationToken { get; set; }

        public TimerArgs(float time, CancellationToken cancellationToken)
        {
            Time = time;
            CancellationToken = cancellationToken;
        }
    }
}
