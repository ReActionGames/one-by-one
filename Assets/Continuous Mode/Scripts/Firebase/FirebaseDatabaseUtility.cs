using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public static class FirebaseDatabaseUtility
    {
        private enum State
        {
            Uninitialized,
            Initializing,
            Initialized
        }

        private const string databaseURL = "https://one-by-one-80012542.firebaseio.com/";

        private static State state = State.Uninitialized;
        private static DatabaseReference dbReference;
        private static FirebaseApp app;

        private static List<Action> onInitializedActions = new List<Action>();

        private static void Initialize(Action onComplete)
        {
            if (state == State.Initialized)
            {
                onComplete.Invoke();
                return;
            }

            if (state == State.Initializing)
            {
                onInitializedActions.Add(onComplete);
                return;
            }

            state = State.Initializing;
            onInitializedActions.Add(onComplete);

            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                DependencyStatus dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    app = Firebase.FirebaseApp.DefaultInstance;

                    // Set a flag here to indicate whether Firebase is ready to use by your app.

                    app.SetEditorDatabaseUrl(databaseURL);
                    dbReference = FirebaseDatabase.DefaultInstance.RootReference;

                    foreach (Action action in onInitializedActions)
                    {
                        action.Invoke();
                    }

                    state = State.Initialized;
                }
                else
                {
                    UnityEngine.Debug.LogError(System.String.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                    state = State.Uninitialized;
                    onInitializedActions.Clear();
                }
            });
        }


        public static void SaveData(string parentNode, string json)
        {
            Initialize(() => InitializedSaveData(parentNode, json));
        }

        private static void InitializedSaveData(string parentNode, string json)
        {
            dbReference.Child(parentNode).SetRawJsonValueAsync(json);
        }


        public static void GetDataAsJson(string node, Action<string> onComplete)
        {
            Initialize(() => InitializedGetDataAsJson(node, onComplete));
        }

        private static void InitializedGetDataAsJson(string node, Action<string> onComplete)
        {
            dbReference.GetValueAsync().ContinueWith(task =>
            {
                //Debug.Log("Received task...");
                if (task.IsFaulted)
                {
                    Debug.LogError("Error retrieving data:" + task.Exception.ToString());
                }
                else if (task.IsCompleted)
                {
                    //Debug.Log("Applying data(1)...");
                    DataSnapshot snapshot = task.Result;
                    //Debug.Log("Applying data(2)...");
                    // Do something with snapshot...
                    string json = snapshot.Child(node).GetRawJsonValue();
                    //Debug.Log($"Applying data(3)...\n{json}");

                    onComplete.Invoke(json);
                }
            });
        }


        public static void SubscribeToValueChanged(string node, EventHandler<ValueChangedEventArgs> onValueChanged)
        {
            Initialize(() => InitializedSubscribeToValueChanged(node, onValueChanged));
        }

        private static void InitializedSubscribeToValueChanged(string node, EventHandler<ValueChangedEventArgs> onValueChanged)
        {
            DatabaseReference nodeReference = dbReference.Child(node);
            nodeReference.ValueChanged += onValueChanged;
        }


        public static void UnsubscribeToValueChanged(string node, EventHandler<ValueChangedEventArgs> onValueChanged)
        {
            Initialize(() => InitializedUnsubscribeToValueChanged(node, onValueChanged));
        }

        private static void InitializedUnsubscribeToValueChanged(string node, EventHandler<ValueChangedEventArgs> onValueChanged)
        {
            DatabaseReference nodeReference = dbReference.Child(node);
            nodeReference.ValueChanged -= onValueChanged;
        }
    }
}