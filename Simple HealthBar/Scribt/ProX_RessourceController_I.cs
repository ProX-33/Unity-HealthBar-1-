using UnityEngine;
using UnityEngine.UI;


public class ProX_RessourceController_I : MonoBehaviour
{
    [Header("Manual Inputs")]
    public Image FillAmount;

    [Header("PRIVATE")]
    [Range(0,1)]
    public float Health_Slider;
    public float HealthCurrent;
    public bool HealthSettings_B;
    public bool RespawnPlayer;
    public float RespawnCountDowm;
    public int Deaths = 0;
    public bool DamageDamping_Step;
    public float DamageStep;



    [Header("Controll Base")]
    public float HealthRain = 2.5f;
    public float HealthMaximum = 100.0f;
    public float RespawnTime = 3;
    public KeyCode AddHealth;
    public KeyCode RemoveHealth;
    public float Damage;
    public float Heal;


    //Damage Damping
    public bool DamageDamping;
    public float DamageStepSpeed;
    

   




    void Start()
    {
        HealthCurrent = HealthMaximum;
        HealthSettings_B = true;
        RespawnTime = 3;
        RespawnCountDowm = 3;

    }


    void Update()
    {
        if(HealthSettings_B)
        {
            Health_Settings();
        }
        if (RespawnPlayer)
        {
            HealthSettings_B = false;
            if (RespawnCountDowm > 0)
            {
                RespawnCountDowm -= Time.deltaTime;
                if (RespawnCountDowm <= 0)
                {
                    Deaths -= 1;
                    RespawnCountDowm = RespawnTime;
                    HealthCurrent = HealthMaximum;
                    print("Player Respawn, Position...City..Level...");
                    HealthSettings_B = true;
                    RespawnPlayer = false;
                }
            }
        }
    }
    public void Health_Settings()
    {
        if(HealthCurrent < 0)
        {
            print("Player Death...Respawn??!");
            RespawnPlayer = true;
        }
        if(HealthCurrent > 0 && HealthCurrent <= HealthMaximum)
        {
            //Health Auto Setting
            HealthCurrent += HealthRain * Time.deltaTime;

            //Health Interaction
            if(Input.GetKeyDown(AddHealth) && HealthCurrent < HealthMaximum)
            {

                HealthCurrent += Heal;
                print("HealValue " + Heal);

                

            }
          
            
            if(Input.GetKeyDown(RemoveHealth))
            {
                if (!DamageDamping && !DamageDamping_Step)
                {
                    HealthCurrent -= Damage;
                    print("Damaged by ... " + Damage);
                }
                if (DamageDamping)
                {
                    DamageStep += Damage;
                    DamageDamping_Step = true;
                }
                

            }
            if (DamageDamping_Step)
            {
                DamageStep -= Time.deltaTime * DamageStepSpeed;
                if (DamageStep > 0)
                {
                    HealthCurrent -= Time.deltaTime * DamageStepSpeed;
                }
                if (DamageStep <= 0)
                {
                    DamageStep = 0;
                    DamageDamping_Step = false;
                }
            }

        }
        if(HealthCurrent > HealthMaximum)
        {
            HealthCurrent = HealthMaximum;
        }
        if (HealthCurrent == HealthMaximum && Input.GetKeyDown(AddHealth))
        {
            print("Full Health");
        }

        FillAmount.fillAmount = Health_Slider;
        Health_Slider = HealthCurrent / HealthMaximum;
    }
}
