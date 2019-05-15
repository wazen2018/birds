using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineManager : MonoBehaviour {

    public GameObject PipelineTemplate;
    public float speed=2f;

    List<Pipeline> pipelines = new List<Pipeline>();

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Coroutine runner = null;
    public void StartRun()
    {
        runner = StartCoroutine(GeneratePipelines());
       
    }

    public void Stop()
    {
        StopCoroutine(runner);
        for(int i = 0; i < pipelines.Count; i++)
        {
            //  StopCoroutine(runner);
            pipelines[i].enabled = false;
        }
     

    }

    //携程？
    IEnumerator GeneratePipelines()
    {
        //while (true)
        for ( int i=0 ;  i<3 ;  i++)
        {
            if(pipelines.Count<3)
                  GeneratePipeline();
            else
            {
                pipelines[i].enabled = false;
                pipelines[i].Init();
            }
            yield return new WaitForSeconds(speed);
        }
    }

    void GeneratePipeline()
    {
        if (pipelines.Count < 3)
        {
            GameObject obj= Instantiate(PipelineTemplate, this.transform);
            Pipeline p = obj.GetComponent<Pipeline>();
            pipelines.Add(p);
        }
        

    }

    public void Init()
    {
        for(int i = 0; i < pipelines.Count; i++)
        {
            Destroy(pipelines[i].gameObject);
        }
        pipelines.Clear();
    }
}
