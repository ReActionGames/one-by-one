﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace ReActionGames.Events
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<string, MessageEvent> eventDictionary;

        private static EventManager eventManager;

        public static EventManager instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                    if (!eventManager)
                    {
                        Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                    }
                    else
                    {
                        eventManager.Init();
                    }
                }

                return eventManager;
            }
        }

        //private void Awake()
        //{
        //    Init();
        //}

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<string, MessageEvent>();
            }
        }

        public static void StartListening(string eventName, UnityAction<Message> listener)
        {
            MessageEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new MessageEvent();
                thisEvent.RemoveListener(listener);
                thisEvent.AddListener(listener);
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, UnityAction<Message> listener)
        {
            if (instance == null)
                return;
            MessageEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(string eventName, Message message = null)
        {
            MessageEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(message);
            }
        }
    }

    [System.Serializable]
    public class MessageEvent : UnityEvent<Message>
    {
    }

    [System.Serializable]
    public class Message
    {
        public object Data
        {
            get; private set;
        }

        public static Message None
        {
            get
            {
                return new Message();
            }
        }

        public Message()
        {
            Data = null;
        }

        public Message(object data)
        {
            Data = data;
        }
    }
}