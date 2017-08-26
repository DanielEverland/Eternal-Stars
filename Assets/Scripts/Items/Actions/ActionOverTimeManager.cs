using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOverTimeManager : MonoBehaviour {

    private static List<EntryBase<float>> floatEntries = new List<EntryBase<float>>();

    private void Update()
    {
        floatEntries.ForEach(x => x.Tick());
    }

    public static void AddFloatEntry(Action<float> obj, float value, float totalTime)
    {
        floatEntries.Add(new FloatEntry(obj, value, totalTime));
    }

    private class FloatEntry : EntryBase<float>
    {
        public FloatEntry(Action<float> obj, float value, float totalTime) : base(obj, value, totalTime)
        {
        }

        protected override List<EntryBase<float>> Entries { get { return floatEntries; } }

        protected override void DoAction()
        {
            obj(value * multiplier);
        }
    }
    private abstract class EntryBase<T>
    {
        public EntryBase(Action<T> obj, T value, float totalTime)
        {
            this.obj = obj;
            this.value = value;
            this.totalTime = totalTime;

            _id = Guid.NewGuid();
        }
        private EntryBase() { }

        protected readonly Action<T> obj;
        protected readonly T value;
        protected readonly float totalTime;

        protected float multiplier { get { return TICK_INTERVAL / totalTime; } }

        protected const float TICK_INTERVAL = 0.1f;

        private readonly Guid _id;
        
        private float deltaTime;
        private float totalElapsed;

        public void Tick()
        {
            deltaTime += Time.deltaTime;
            totalElapsed += Time.deltaTime;

            while (deltaTime > TICK_INTERVAL)
            {
                deltaTime -= TICK_INTERVAL;

                DoAction();
            }

            if(totalElapsed >= totalTime)
            {
                Entries.Remove(this);
            }
        }

        protected abstract List<EntryBase<T>> Entries { get; }
        protected abstract void DoAction();
        
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return obj.GetHashCode() == GetHashCode();
        }
        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("Obj: {0}, Value: {1}\nElapsed Time: {2}, TotalTime: {3}", obj, value, totalElapsed, totalTime);
        }
    }
}
