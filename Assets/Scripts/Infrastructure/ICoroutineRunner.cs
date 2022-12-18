using System.Collections;
using UnityEngine;

namespace OnlineChess.Scripts.Infrastructure
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}