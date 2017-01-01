using UnityEngine;
using System.Collections;

public class UnitySingleton<T> : MonoBehaviour where T :MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObj = GameObject.Find(Define.SingletonName);

                if(singletonObj == null)
                {
                    singletonObj = new GameObject(Define.SingletonName);
                    DontDestroyOnLoad(singletonObj);
                }

                T targetT = singletonObj.GetComponent<T>();

                if(targetT == null)
                {
                    targetT = singletonObj.AddComponent<T>();
                }

                instance = targetT;
            }

            return instance;
        }
    }

	void Awake()
	{
		OnAwake ();
	}

	void Start()
	{
		OnStart ();
	}

	void Update()
	{
		OnUpdate ();
	}

	void OnEnable()
	{
		OnOpen ();
	}

	void OnDisable()
	{
		OnClose ();
	}


	protected virtual void OnAwake()
	{
		
	}

	protected virtual void OnStart()
	{
		
	}

	protected virtual void OnUpdate()
	{
		
	}

	protected virtual void OnOpen ()
	{
		
	}

	protected virtual void OnClose ()
	{
		
	}
}
