using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEventController : MonoBehaviour {

	public Button build_button;
	public Button build_house_button;
    public Button build_canteen_button;
    public Button build_training_ground_button;
	public GameObject build_panel;
	public GameObject house_prefab;
    public GameObject canteen_prefab;
    public GameObject training_ground_prefab;


	// Use this for initialization
	void Start () {
		build_button.onClick.AddListener (OnBuildButtonClicked);
		build_house_button.onClick.AddListener (OnBuildHouseButtonClicked);
        build_canteen_button.onClick.AddListener (OnBuildCanteenButtonClicked);
        build_training_ground_button.onClick.AddListener(OnBuildTrainingGroundButtonButtonClicked);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnBuildButtonClicked()
	{
		build_panel.SetActive ( !build_panel.activeSelf );
	}

	void OnBuildHouseButtonClicked()
	{
		build_panel.SetActive ( false );
		GameObject build_helper = GameObject.Find ("BuildHelper");
		if (build_helper)
		{
			BuildHelper helper = build_helper.GetComponent<BuildHelper>();
			helper.EnableBuildMode(house_prefab);
		}
	}

    void OnBuildCanteenButtonClicked()
    {
        build_panel.SetActive ( false );
        GameObject build_helper = GameObject.Find ("BuildHelper");
        if (build_helper)
        {
            BuildHelper helper = build_helper.GetComponent<BuildHelper>();
            helper.EnableBuildMode(canteen_prefab);
        }
    }

    void OnBuildTrainingGroundButtonButtonClicked()
    {
        build_panel.SetActive ( false );
        GameObject build_helper = GameObject.Find ("BuildHelper");
        if (build_helper)
        {
            BuildHelper helper = build_helper.GetComponent<BuildHelper>();
            helper.EnableBuildMode(training_ground_prefab);
        }
    }
}
