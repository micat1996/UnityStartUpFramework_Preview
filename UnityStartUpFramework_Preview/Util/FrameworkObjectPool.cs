using System;
using System.Collections.Generic;


namespace UnityStartUpFramework.Util
{
    // 오브젝트 풀링을 수행하는 클래스
    /// - 풀링 대상을 형식 매개 변수 T 에 전달한다.
    /// - 풀링 대상은 IObjectPoolable 인터페이스를 구현해야한다.
    public sealed class ObjectPool<T> 
        where T : class, IObjectPoolable
    {
        // 풀링시킬 객체들을 참조할 리스트
        private List<T> _PoolObjects = new List<T>();
        public List<T> poolObjects => _PoolObjects;

        public T RegisterRecyclableObject(T newRecyclableObject)
        {
            _PoolObjects.Add(newRecyclableObject);
            return newRecyclableObject;
        }


        public T GetRecycleObject() =>
            GetRecycleObject((T poolableObject) => poolableObject.canRecyclable);

        // 재활용된 오브젝트를 얻습니다.
        public T GetRecycleObject(Func<T,bool> pred)
        {
            // 재사용 가능한 오브젝트가 존재하는지 검사합니다.
            T recycleableObject = _PoolObjects.Find((T poolableObject) => pred(poolableObject));

            if (recycleableObject == null)
                return null;

            recycleableObject.onRecycleStartEvent?.Invoke();

            // 재사용 불가능한 상태로 설정합니다.
            recycleableObject.canRecyclable = false;

            // 찾은 객체를 반환합니다.
            return recycleableObject;

        }


    }
}
