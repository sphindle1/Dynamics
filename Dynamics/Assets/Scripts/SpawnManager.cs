using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class SpawnManager : MonoBehaviour
{
   #region Editor Variables
   [SerializeField]
   [Tooltip("The different types of enemies that should be spawned and their corresponding spawn information.")]
   private EnemySpawnInfo[] m_EnemyTypes;
   #endregion

   #region Non-Editor Variables
   /* A timer for each enemy that should spawn an enemy of the corresponding
    * type when it reaches 0. Additionally, upon reaching 0, the value of the
    * timer should be reset to the appropriate value based on the enemy's spawn
    * rate. Therefore, we will have infinite spawning of the enemies.
    * 
    * Some challenges:
    * - Implement the spawning using a coroutine instead of this using this way
    * - Make the spawn rate ramp up (may require creating a mutator in the EnemySpawnInfo struct)
    */
   private float[] m_EnemySpawnTimers;
    private int count;
   private float[] timeStamps;
    private float startTime;
    private float endTime;
   #endregion

   #region First Time Initialization and Set Up
   private void Awake()
   {
        // Initialize the spawn timers using the FirstSpawnTime variable
        //m_EnemySpawnTimers = new float[m_EnemyTypes.Length];
        timeStamps = new float[]{ 0, 0.9f, 1.32f, 1.73f, 2.15f, 2.36f, 2.56f, 2.98f, 3.39f, 4.21f, 4.63f,
            5.04f, 5.46f, 5.66f, 5.88f, 6.28f, 6.70f, 7.52f, 7.95f, 8.35f, 8.77f, 8.97f, 9.18f, 9.61f, 10.01f,
            10.84f, 11.24f, 11.67f, 12.08f, 12.29f, 12.49f, 12.89f, 13.31f, 14.13f, 14.58f, 14.97f, 15.435f,
            15.59f, 15.83f, 16.21f, 16.64f, 17.46f, 17.87f, 18.28f, 18.69f, 18.91f, 19.12f, 19.51f, 19.95f,
            20.15f, 20.77f, 21.19f, 21.60f, 22.00f, 22.21f, 22.42f, 23.25f, 24.08f, 24.49f, 24.91f, 25.32f,
            25.53f, 25.74f, 26.13f, 26.55f, 33.18f, 33.99f, 34.44f, 34.85f, 35.67f, 36.11f, 36.47f, 39.77f,
            40.63f, 41.05f, 43.09f, 43.93f, 44.37f, 46.43f, 47.25f, 47.65f, 48.06f, 48.92f, 49.73f, 50.57f,
            51.42f, 53.03f, 53.85f, 54.67f, 56.35f, 57.16f, 59.65f, 66.27f, 67.92f, 69.54f, 70.86f, 72.87f,
            74.53f, 76.15f, 79.50f, 80.33f, 81.16f, 81.98f, 82.77f, 84.15f, 84.52f, 85.36f, 85.11f, 87.76f,
            89.44f, 90.22f, 90.62f, 91.12f, 92.76f, 33.59f, 94.01f, 94.41f, 94.84f, 95.02f, 95.25f, 95.66f,
            96.05f, 96.87f, 97.31f, 97.73f, 98.13f, 98.34f, 98.56f, 98.97f, 99.39f, 100.22f, 100.62f, 101.02f,
            101.44f, 101.64f, 101.84f, 102.24f, 102.68f, 103.50f, 103.93f, 104.32f, 104.77f, 104.94f, 105.16f,
            105.59f, 105.98f, 106.82f, 107.23f, 107.62f, 108.00f, 108.26f, 108.47f, 108.89f, 109.3f, 110.1f,
            110.43f, 110.95f, 111.37f, 111.55f, 111.77f, 112.2f, 112.59f, 113.43f, 113.83f, 114.26f, 114.67f,
            114.87f, 115.09f, 115.53f, 115.9f, 116.7f, 117.15f, 117.58f, 117.98f, 118.18f, 118.39f, 118.81f,
            119.22f, 120.04f, 120.45f, 120.88f, 121.31f, 121.49f, 121.71f, 122.11f, 122.56f, 123.36f, 123.78f,
            124.20f, 124.60f, 124.80f, 125.02f, 125.33f, 126.66f, 127.1f, 127.49f, 127.82f, 128.12f, 128.34f,
            128.64f, 129.15f, 129.98f, 130.38f, 130.8f, 131.23f, 131.43f, 131.63f, 132.06f, 132.47f, 133.27f,
            133.73f, 134.14f, 134.53f, 134.74f, 134.97f, 135.36f, 135.77f, 136.6f, 137.04f, 137.43f, 137.86f,
            138.67f, 139.16f, 139.92f, 140.73f, 141.17f, 141.37f, 141.59f, 141.99f, 142.39f, 143.24f, 143.64f,
            144.04f, 144.48f, 144.69f, 144.88f, 145.29f, 145.71f, 146.54f, 157.73f, 157.94f, 158.13f, 158.55f,
            158.99f, 165.59f, 171.39f, 171.78f, 172.18f, 173.06f, 173.44f, 173.85f, 174.27f, 174.43f, 174.73f,
            175.09f, 175.53f, 176.31f, 176.74f, 177.17f, 178.09f, 178.39f, 178.81f, 182.13f, 182.95f, 183.36f,
            183.79f, 184.62f, 185.01f, 185.44f, 192.08f, 192.91f, 193.32f, 193.72f, 194.57f, 194.95f, 195.36f,
            196.2f, 196.6f, 197.04f, 197.84f, 198.26f, 198.68f, 199.51f, 199.93f, 200.32f, 200.76f, 200.97f,
            201.17f, 201.58f, 201.99f, 202.82f, 203.22f, 203.64f, 204.07f, 204.27f, 204.49f, 204.88f, 205.3f,
            206.13f, 206.54f, 206.95f, 207.37f, 207.58f, 207.79f, 208.2f, 208.62f, 209.44f, 209.85f, 210.27f,
            210.68f, 210.9f, 211.1f, 211.52f, 211.96f };
        count = 0;
        endTime = 7.0f;
        startTime = Time.time;
        /*for (int i = 0; i < m_EnemyTypes.Length; i++)
        {
            m_EnemySpawnTimers[i] = timeStamps[count];
        }*/
   }
   #endregion

   #region Main Updates
   private void Update()
   {
      // You may want to use either a foreach or for loop (for scalability)
      // Check if its time to spawn a particular enemy
      // If it is, just spawn the enemy using Instantiate(m_EnemyTypes[i].EnemyPrefab)
         // Make sure to reset the timer back to the appropriate value based on SpawnRate
      // Else, increase the timer using Time.deltaTime
      if (count >= timeStamps.Length)
        {
            if (endTime <= 0)
            {
                SceneManager.LoadScene("endscreen");
            }
            endTime -= Time.deltaTime;
        }
      else if (timeStamps[count] < Time.time - startTime)
        {
            Instantiate(m_EnemyTypes[0].EnemyPrefab);
            count++;
        }
      /*for (int i = 0; i < m_EnemySpawnTimers.Length; i++)
      {
          if (m_EnemySpawnTimers[i] <= 0)
          {
                //m_EnemySpawnTimers[i] = m_EnemyTypes[i].SpawnRate;
                if (count < timeStamps.Length)
                {
                    Instantiate(m_EnemyTypes[i].EnemyPrefab);
                    count++;
                    m_EnemySpawnTimers[i] = timeStamps[count] - timeStamps[count - 1] + m_EnemySpawnTimers[i];
                }
          } else
            {
                m_EnemySpawnTimers[i] -= Time.deltaTime;
            }
      }*/
   }
   #endregion
}

[System.Serializable]
public struct EnemySpawnInfo
{
   #region Editor Variables
   [SerializeField]
   [Tooltip("The enemy prefab to spawn. This is what will be instantiated each time.")]
   private GameObject m_EnemyPrefab;

   [SerializeField]
   [Tooltip("The time we should wait before the first enemy is spawned.")]
   private float m_FirstSpawnTime;

   /*[SerializeField]
   [Range(0, 100)]
   [Tooltip("How many enemies should spawn per second.")]
   private float m_SpawnRate;*/
   #endregion

   #region Accessors and Mutators
   public GameObject EnemyPrefab
   {
      get { return m_EnemyPrefab; }
   }

   public float FirstSpawnTime
   {
      get { return m_FirstSpawnTime; }
   }

   // Doing (1 / SpawnRate) might be more useful than directly using SpawnRate
   /*public float SpawnRate
   {
      get { return m_SpawnRate; }
   }*/
   #endregion
}