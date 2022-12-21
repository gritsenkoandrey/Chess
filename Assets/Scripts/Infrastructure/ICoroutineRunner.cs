using System.Collections;
using UnityEngine;

namespace OnlineChess.Infrastructure
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}