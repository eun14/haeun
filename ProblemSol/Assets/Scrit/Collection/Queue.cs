using UnityEngine;

public class Node<T>
{
    public T data;
    public Node<T> next;

    public Node(T data)
    {
        this.data = data;
        this.next = null;
    }
}

public class Queue<T>
{
    private Node<T> front;
    private Node<T> rear;

    public Queue()
    {
        front = null;
        rear = null;
    }

    public void Enqueue(T data)
    {
        Node<T> newNode = new Node<T>(data);
        if (rear == null)
        {
            front = newNode;
            rear = newNode;
        }
        else
        {
            rear.next = newNode;
            rear = newNode;
        }
    }

    public T Dequeue()
    {
        if (front == null)
        {
            Debug.LogError("Queue is empty");
            return default(T);
        }

        T data = front.data;
        front = front.next;
        if (front == null)
        {
            rear = null;
        }
        return data;
    }

    public bool IsEmpty()
    {
        return front == null;
    }

    public T Peek()
    {
        if (front == null)
        {
            Debug.LogError("Queue is empty");
            return default(T);
        }
        return front.data;
    }
}


