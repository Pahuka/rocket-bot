using System.Collections.Generic;

namespace rocket_bot
{
    public class Channel<T> where T : class
    {
        List<T> result = new List<T>();

        /// <summary>
        /// Возвращает элемент по индексу или null, если такого элемента нет.
        /// При присвоении удаляет все элементы после.
        /// Если индекс в точности равен размеру коллекции, работает как Append.
        /// </summary>
        public T this[int index]
        {
            get
            {
                lock (result)
                {
                    var item = (index < Count) ? result[index] : null;
                    return item;
                }
            }
            set
            {
                lock (result)
                {
                    if (index == Count) result.Add(value);
                    else
                    {
                        result.RemoveRange(index, Count - index);
                        result.Add(value);
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает последний элемент или null, если такого элемента нет
        /// </summary>
        public T LastItem()
        {
            lock (result)
            {
                var item = (Count > 0) ? result[Count - 1] : null;
                return item;
            }
        }

        /// <summary>
        /// Добавляет item в конец только если lastItem является последним элементом
        /// </summary>
        public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
        {
            lock (result)
                if (LastItem() == knownLastItem) result.Add(item);
        }

        /// <summary>
        /// Возвращает количество элементов в коллекции
        /// </summary>
        public int Count
        {
            get
            {
                lock (result) return result.Count;
            }
        }
    }
}