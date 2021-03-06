﻿using System.Collections.Generic;
using UnityEngine;

namespace Palomas.Menu
{
    public class RequestsViewController : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private Transform ViewHolder;

        [SerializeField]
        private GameObject ViewPrefab;

        private IDictionary<string, RequestView> RequestViews;

        private void Start()
        {
            RequestViews = new Dictionary<string, RequestView>();

            GameEvents.RequestObtained += (sender, args) => AddRequestToView(args);
            GameEvents.RequestCompleted += (sender, args) => SetRequestAsCompleted(args);
        }

        private void AddRequestToView(RequestEventArgs args)
        {
            if(!RequestViews.ContainsKey(args.RequestId))
            {
                GameObject viewObject = GameObject.Instantiate(ViewPrefab, ViewHolder);
                RequestView requestView = viewObject.GetComponent<RequestView>();
                requestView.SetRequestId(args.RequestId);

                RequestViews.Add(args.RequestId, requestView);
            }
        }

        private void SetRequestAsCompleted(RequestItemCompletedEventArgs args)
        {
            //TODO animate
            if(RequestViews.TryGetValue(args.RequestId, out RequestView requestView))
            {
                RequestViews.Remove(args.RequestId);
                Destroy(requestView.gameObject);
            }
        }
    }
}
