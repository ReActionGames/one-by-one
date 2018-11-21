using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Continuous
{
    public enum EventNames
    {
        GameStart,
        GameEnd,
        GameRestart
    }

    public static class EventManager
    {
        private static Dictionary<EventNames, MessageEvent> eventDictionary;

        public static void StartListening(EventNames eventName, UnityAction<Message> listener)
        {
            InitiateEventDictionaryIfNull();

            MessageEvent thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new MessageEvent();
                thisEvent.RemoveListener(listener);
                thisEvent.AddListener(listener);
                eventDictionary.Add(eventName, thisEvent);
            }
        }

        private static void InitiateEventDictionaryIfNull()
        {
            if(eventDictionary != null)return;

            eventDictionary = new Dictionary<EventNames, MessageEvent>();
        }

        public static void StopListening(EventNames eventName, UnityAction<Message> listener)
        {
            InitiateEventDictionaryIfNull();

            MessageEvent thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(EventNames eventName, Message message)
        {
            InitiateEventDictionaryIfNull();

            MessageEvent thisEvent = null;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(message);
            }
        }

        public static void TriggerEvent(EventNames eventName)
        {
            InitiateEventDictionaryIfNull();

            TriggerEvent(eventName, Message.None);
        }
    }

    [System.Serializable]
    public class MessageEvent : UnityEvent<Message>
    {
    }

    [System.Serializable]
    public struct Message
    {
        public object Data { get; private set; }

        public static Message None => new Message(null);

        public Message(object data)
        {
            Data = data;
        }
    }
}