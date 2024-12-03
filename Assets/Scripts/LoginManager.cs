using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.Networking; // Para UnityWebRequest

public class LoginManager : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text loginMessage;

    private string apiUrl = "http://127.0.0.1:3000/user/login"; // URL de la API para login

    public void OnLoginButtonClicked()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        StartCoroutine(LoginUser(email, password));
    }

    private IEnumerator LoginUser(string email, string password)
    {
        // Crea un objeto con los datos de login
        LoginData userData = new LoginData
        {
            correo = email,
            password = password
        };

        // Convierte los datos a JSON
        string json = JsonUtility.ToJson(userData);

        // Crea una solicitud POST usando UnityWebRequest
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Enviar la solicitud y esperar la respuesta
        yield return request.SendWebRequest();

        // Manejar errores de la solicitud
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error en la solicitud: " + request.error);
            loginMessage.text = "Error al conectar con el servidor.";
        }
        else
        {
            // Imprime la respuesta del servidor
            Debug.Log("Respuesta del servidor: " + request.downloadHandler.text);

            // Intenta deserializar la respuesta JSON
            try
            {
                LoginResponse data = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);

                if (data != null && data.success)
                {
                    // Guarda el ID del usuario
                    PlayerPrefs.SetInt("userId", data.userData.id_usuario);

                    int userId = PlayerPrefs.GetInt("userId");
                    Debug.Log("ID de usuario guardado: " + userId);
                    loginMessage.text = "Login exitoso";
                }
                else
                {
                    loginMessage.text = (data != null ? data.message : "Respuesta no válida del servidor");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error al deserializar JSON: " + ex.Message);
                loginMessage.text = "Error en la respuesta del servidor.";
            }
        }
    }
}

// Clase para enviar los datos de login
[System.Serializable]
public class LoginData
{
    public string correo;
    public string password;
}

// Clase para deserializar la respuesta del servidor
[System.Serializable]
public class LoginResponse
{
    public bool success;
    public UserData userData;
    public string message;
}

[System.Serializable]
public class UserData
{
    public int id_usuario;
    public string nombre;
}
