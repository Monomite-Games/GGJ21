﻿using System;
using UnityEngine;

namespace Palomas
{
    public class GameEvents : MonoBehaviour
    {
        #region Singleton
        public static GameEvents Instance
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

        private void Awake()
        {
            CreateSingleton();
        }

        public event EventHandler ToMainMenu;
        public event EventHandler ToPauseMenu;
        public event EventHandler BackFromPauseMenu;
        public event EventHandler RestartLevel;

        public event EventHandler GamePrepared;
        public event EventHandler GameStart;
        public event EventHandler<GameEndEventArgs> GameEnd;

        public event EventHandler<RequestEventArgs> RequestObtained;
        public event EventHandler<RequestItemCompletedEventArgs> RequestCompleted;
        public event EventHandler<RequestItemEventArgs> RequestChanged;
        public event EventHandler<ItemEventArgs> ItemDelivered;
        public event EventHandler<ItemEventArgs> ItemAttached;
        public event EventHandler<int> PointsChanged;
        public event EventHandler LifeLost;
        public event EventHandler<ShitMeterEventArgs> ShitMeterChanged;
        public event EventHandler Shit;

        public void OnToMainMenu()
        {
            ToMainMenu?.Invoke(this, EventArgs.Empty);
        }

        public void OnToPauseMenu()
        {
            ToPauseMenu?.Invoke(this, EventArgs.Empty);
        }

        public void OnBackFromPauseMenu()
        {
            BackFromPauseMenu?.Invoke(this, EventArgs.Empty);
        }

        public void OnRestartLevel()
        {
            RestartLevel?.Invoke(this, EventArgs.Empty);
        }

        public void OnGamePrepared()
        {
            GamePrepared?.Invoke(this, EventArgs.Empty);
        }

        public void OnGameStart()
        {
            GameStart?.Invoke(this, EventArgs.Empty);
        }

        public void OnGameEnd(GameEndState endState)
        {
            GameEndEventArgs eventArgs = new GameEndEventArgs(endState);
            GameEnd?.Invoke(this, eventArgs);
        }

        public void OnRequestObtained(string requestId)
        {
            RequestEventArgs eventArgs = new RequestEventArgs(requestId);
            RequestObtained?.Invoke(this, eventArgs);
        }

        public void OnRequestCompleted(string requestId, string itemId, int requestPoints)
        {
            RequestItemCompletedEventArgs eventArgs = new RequestItemCompletedEventArgs(requestId, itemId, requestPoints);
            RequestCompleted?.Invoke(this, eventArgs);
        }

        public void OnRequestChanged(string requestId, string itemId, int spawnLevel)
        {
            RequestItemEventArgs eventArgs = new RequestItemEventArgs(requestId, itemId, spawnLevel);
            RequestChanged?.Invoke(this, eventArgs);
        }

        public void OnItemDelivered(string itemId)
        {
            ItemEventArgs eventArgs = new ItemEventArgs(itemId);
            ItemDelivered?.Invoke(this, eventArgs);
        }

        public void OnItemAttached(string itemId)
        {
            ItemEventArgs eventArgs = new ItemEventArgs(itemId);
            ItemAttached?.Invoke(this, eventArgs);
        }

        public void OnPointsChanged(int currentPoints)
        {
            PointsChanged?.Invoke(this, currentPoints);
        }

        public void OnLifeLost()
        {
            LifeLost?.Invoke(this, EventArgs.Empty);
        }

        public void OnShitMeterChanged(int currentValue)
        {
            ShitMeterEventArgs eventArgs = new ShitMeterEventArgs(currentValue);
            ShitMeterChanged?.Invoke(this, eventArgs);
        }

        public void OnShit()
        {
            Shit?.Invoke(this, EventArgs.Empty);
        }
    }

    public class GameEndEventArgs
    {
        public GameEndState EndState
        {
            get;
            private set;
        }

        public GameEndEventArgs(GameEndState endState)
        {
            this.EndState = endState;
        }
    }

    public class RequestEventArgs
    {
        public string RequestId
        {
            get;
            private set;
        }

        public RequestEventArgs(string requestId)
        {
            this.RequestId = requestId;
        }
    }

    public class ItemEventArgs
    {
        public string ItemId
        {
            get;
            private set;
        }

        public ItemEventArgs(string itemId)
        {
            this.ItemId = itemId;
        }
    }

    public class RequestItemEventArgs
    {
        public string RequestId
        {
            get;
            private set;
        }

        public string ItemId
        {
            get;
            private set;
        }

        public int SpawnLevel
        {
            get;
            private set;
        }

        public RequestItemEventArgs(string requestId, string itemId, int spawnLevel)
        {
            this.RequestId = requestId;
            this.ItemId = itemId;
            this.SpawnLevel = spawnLevel;
        }
    }

    public class RequestItemCompletedEventArgs
    {
        public string RequestId
        {
            get;
            private set;
        }

        public string ItemId
        {
            get;
            private set;
        }

        public int RequestPoints
        {
            get;
            private set;
        }

        public RequestItemCompletedEventArgs(string requestId, string itemId, int requestPoints)
        {
            this.RequestId = requestId;
            this.ItemId = itemId;
            this.RequestPoints = requestPoints;
        }
    }

    public class HealthEventArgs
    {
        public int CurrentHealth
        {
            get;
            private set;
        }

        public int HealthRemoved
        {
            get;
            private set;
        }

        public HealthEventArgs(int currentHealth, int healthRemoved)
        {
            this.CurrentHealth = currentHealth;
            this.HealthRemoved = healthRemoved;
        }
    }

    public class ShitMeterEventArgs
    {
        public int CurrentValue
        {
            get;
            private set;
        }

        public ShitMeterEventArgs(int currentValue)
        {
            this.CurrentValue = currentValue;
        }
    }
}