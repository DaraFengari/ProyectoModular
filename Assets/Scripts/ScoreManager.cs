using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text;
using Unity.VisualScripting;

public class ScoreManager : MonoBehaviour
{
    private string apiUrl = "http://127.0.0.1:3000/user/puntuaciones";

    public void OnGameOver(int puntaje, int nhabitaciones, int horas, int minutos, int segundos, int enemigosDerrotados)
    {
        int userId = PlayerPrefs.GetInt("userId");
        if(userId != 0) {
            StartCoroutine(SendScore(userId, puntaje, nhabitaciones, horas, minutos, segundos, enemigosDerrotados));
        }
        else
        {
            Debug.LogError("No se ha encontrado el ID de usuario. El jugador no está logueado.");
        }
            

           
    }

    private IEnumerator SendScore(int id_usuario, int puntaje, int nhabitaciones, int horas, int minutos, int segundos, int enemigosDerrotados)
    {
        // Crea un objeto con los datos de la puntuación
        ScoreData scoreData = new ScoreData
        {
            idusuario = id_usuario,
            puntaje = puntaje,
            nhabitaciones = nhabitaciones,
            horas = horas,
            minutos = minutos,
            segundos = segundos,
            enemigosderro = enemigosDerrotados
        };

        // Convierte el objeto a JSON
        string json = JsonUtility.ToJson(scoreData);

        // Crea la solicitud POST
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Envía la solicitud y espera la respuesta
        yield return request.SendWebRequest();

        // Manejar la respuesta del servidor
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error al guardar la puntuación: " + request.error);
        }
        else
        {
            Debug.Log("Puntuación guardada exitosamente: " + request.downloadHandler.text);
        }
    }
}

// Clase para enviar los datos de puntuación
[System.Serializable]
public class ScoreData
{
    public int idusuario;
    public int puntaje;
    public int nhabitaciones;
    public int horas;
    public int minutos;
    public int segundos;
    public int enemigosderro;
}


