#include <vector>

template <class T>
class PriorityQueue {

   private:
    std::vector<T> queue;
    void Heapify(int idx);
    void swap(int idx1, int idx2);

   public:
    PriorityQueue();
    ~PriorityQueue();

    int Enqueue(T item);
    T Dequeue();

    T Parent(int idx) { return queue[(idx - 1) / 2]; }
    T LeftChild(int idx) { return queue[2 * idx + 1]; }
    T RightChild(int idx) { return queue[2 * idx + 2]; }
    int Size() { return queue.size(); }
    T Peek() { return queue.front(); }

};

template <class T>
void PriorityQueue<T>::Heapify(int idx) {

    int left = LeftChild(idx);
    int right = RightChild(idx);
    int largest = idx;

    if (left < queue.size() && queue[left] > queue[largest]) {
        largest = left;
    }

    if (right < queue.size() && queue[right] > queue[largest]) {
        largest = right;
    }

    if (largest != idx) {
        swap(idx, largest);
        Heapify(largest);
    }
}

template <class T>
void PriorityQueue<T>::swap(int idx1, int idx2) {
    queue.swap(idx1, idx2);
}

template <class T>
PriorityQueue<T>::PriorityQueue() {
    queue = new std::vector<T>();
}

template <class T> 
PriorityQueue<T>::~PriorityQueue() {
    delete queue;
}

template <class T>
int PriorityQueue<T>::Enqueue(T item) {

    queue.push_back(item);

    int idx = queue.size() - 1;
    
    // Heapify up
    while (idx > 0 && queue[idx] > Parent(idx)) {
        swap(idx, (idx - 1) / 2);
        idx = Parent(idx);
    }

    return queue.size() - 1;
}

template <class T> 
T PriorityQueue<T>::Dequeue() {
    T item = queue->front();
    queue->erase(queue->begin());
    return item;
}

