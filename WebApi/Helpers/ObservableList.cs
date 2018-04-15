using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
public delegate void ObservableListChanged<in T>(object sender, T eventArgs = null) where T:class;
namespace WebApi.Helpers
{
    public class ObservableList<T> : IList<T> where T : class {
        private List<T> _collection= new List<T>();
        public event ObservableListChanged<T> OnAdd;
        public event ObservableListChanged<T> OnRemove;
        public event ObservableListChanged<T> OnClear;
        public ObservableList(IList<T>items) {
            foreach (var item in items) {
                this._collection.Add(item);
            }
        }
        public IEnumerator<T> GetEnumerator() {
            return this._collection.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        public void Add(T item) {
           this._collection.Add(item);
            this.OnAdd?.Invoke(this,item);
        }
        public void Clear() {
           this._collection.Clear();
            this.OnClear?.Invoke(this);

        }
        public bool Contains(T item) {
            return this._collection.Contains(item);
        }
        public void CopyTo(T[] array, int arrayIndex) {
            this._collection.CopyTo(array,arrayIndex);
        }
        public bool Remove(T item) {
            var removalResult= this._collection.Remove(item);
            this.OnRemove?.Invoke(this, item);
            return removalResult;
        }
        public int Count { get; }
        public bool IsReadOnly { get; }
        public int IndexOf(T item) {
            return this._collection.IndexOf(item);
        }
        public void Insert(int index, T item) {
            this._collection.Insert(index,item);
        }
        public void RemoveAt(int index) {
            this._collection.RemoveAt(index);
        }
        public T this[int index] {
            get { return this._collection[index]; }
            set { this._collection[index] = value; }
        }
    }
}
