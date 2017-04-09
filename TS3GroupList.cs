using System;
using System.Collections;
using System.Collections.Generic;

namespace Ts3Tray
{
	public class TS3GroupList : IEnumerable<TS3Group>, IEnumerable
	{
		private List<TS3Group> liste = new List<TS3Group>();

		public int Count
		{
			get
			{
				return this.liste.Count;
			}
		}

		public TS3Group this[int groupid]
		{
			get
			{
				foreach (TS3Group current in this.liste)
				{
					if (current.ID == groupid)
					{
						return current;
					}
				}
				return null;
			}
			internal set
			{
				for (int i = 0; i < this.liste.Count; i++)
				{
					if (this.liste[i].ID == groupid)
					{
						this.liste[i] = value;
						return;
					}
				}
				this.liste.Add(value);
			}
		}

		internal void Add(TS3Group item)
		{
			this.liste.Add(item);
		}

		internal void Clear()
		{
			this.liste.Clear();
		}

		public bool Contains(TS3Group item)
		{
			return this.liste.Contains(item);
		}

		public void CopyTo(TS3Group[] array, int arrayIndex)
		{
			this.liste.CopyTo(array, arrayIndex);
		}

		internal bool Remove(TS3Group item)
		{
			return this.liste.Remove(item);
		}

		public IEnumerator<TS3Group> GetEnumerator()
		{
			return this.liste.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.liste.GetEnumerator();
		}

		IEnumerator<TS3Group> IEnumerable<TS3Group>.GetEnumerator()
		{
			return this.liste.GetEnumerator();
		}

		public int IndexOf(TS3Group item)
		{
			return this.liste.IndexOf(item);
		}

		internal void Insert(int index, TS3Group item)
		{
			this.liste.Insert(index, item);
		}

		internal void RemoveAt(int index)
		{
			this.liste.RemoveAt(index);
		}
	}
}
