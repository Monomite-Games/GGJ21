using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Palomas.Requests
{
    public class RequestsList : MonoBehaviour
    {
        #region Singleton
        public static RequestsList Instance
        {
            get;
            private set;
        }
        private void CreateSingleton()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        [SerializeField]
        private List<Request> Requests;

        private IDictionary<string, Request> RequestsMap;

        private void Awake()
        {
            CreateSingleton();
            RequestsMap = new Dictionary<string, Request>();

            FillMap();
        }

        private void FillMap()
        {
            foreach(Request request in Requests)
            {
                RequestsMap.Add(request.GetId(), request);
            }
        }

        public Request GetById(string id)
        {
            if(RequestsMap.TryGetValue(id, out Request request))
            {
                return request;
            }

            return null;
        }

        public Request GetRandomUnused()
        {
            ICollection<Request> unusedRequests = RequestsMap.Values.Where<Request>(request => !request.IsInUse()).ToList<Request>();
            return GameUtils.RandomElement<Request>(unusedRequests);
        }
    }
}
