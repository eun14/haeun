using System;
using System.Collections.Generic;

namespace DataStrucuture
{
    public class StackWithQueue<T>
    {
        private Queue<T> primaryQueue;          // 주 큐
        private Queue<T> secondaryQueue;        // 보조 큐

        public StackWithQueue()
        {
            primaryQueue = new Queue<T>();      // 주 큐 초기화
            secondaryQueue = new Queue<T>();    // 보조 큐 초기화
        }

        public void Push(T data)                // 스택에 요소 추가
        {
            while (primaryQueue.Count > 0)      // 기존에 있는 요소들을 보조 큐로 옮김
            {
                secondaryQueue.Enqueue(primaryQueue.Dequeue());
            }

            primaryQueue.Enqueue(data);         // 새로운 요소를 주 큐에 추가

            while (secondaryQueue.Count > 0)    // 보조 큐의 모든 요소를 다시 주 큐로 옮김
            {
                primaryQueue.Enqueue(secondaryQueue.Dequeue());
            }
        }

        public T Pop()                          // 스택에서 요소를 제거하고 반환
        {
            if (primaryQueue.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }

            return primaryQueue.Dequeue();      // 주 큐에서 가장 앞에 있는 요소를 꺼내 반환
        }
    }
}
