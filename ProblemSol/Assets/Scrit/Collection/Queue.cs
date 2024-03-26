using System;
using System.Collections.Generic;

namespace DataStrucuture
{
    public class StackWithQueue<T>
    {
        private Queue<T> primaryQueue;          // �� ť
        private Queue<T> secondaryQueue;        // ���� ť

        public StackWithQueue()
        {
            primaryQueue = new Queue<T>();      // �� ť �ʱ�ȭ
            secondaryQueue = new Queue<T>();    // ���� ť �ʱ�ȭ
        }

        public void Push(T data)                // ���ÿ� ��� �߰�
        {
            while (primaryQueue.Count > 0)      // ������ �ִ� ��ҵ��� ���� ť�� �ű�
            {
                secondaryQueue.Enqueue(primaryQueue.Dequeue());
            }

            primaryQueue.Enqueue(data);         // ���ο� ��Ҹ� �� ť�� �߰�

            while (secondaryQueue.Count > 0)    // ���� ť�� ��� ��Ҹ� �ٽ� �� ť�� �ű�
            {
                primaryQueue.Enqueue(secondaryQueue.Dequeue());
            }
        }

        public T Pop()                          // ���ÿ��� ��Ҹ� �����ϰ� ��ȯ
        {
            if (primaryQueue.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }

            return primaryQueue.Dequeue();      // �� ť���� ���� �տ� �ִ� ��Ҹ� ���� ��ȯ
        }
    }
}
