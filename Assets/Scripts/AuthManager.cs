using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour
{
    //Variables for firebase 
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    //LoginPanel variables   
    public TMP_InputField L_Email;
    public TMP_InputField L_Password;
    public TMP_Text L_ErrorText;
    

    //SignupPanel variables
    public TMP_InputField S_Name;
    public TMP_InputField S_Email;
    public TMP_InputField S_Password;
    public TMP_InputField S_ConfirmPass;
    public TMP_Text S_ErrorText;

    public UIScript uiscript;
    void Awake()
    {
        //Checking the dependencies and initializing Firebase 
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;

            //If the dependencies are avalible, Initializing Firebase
            if (dependencyStatus == DependencyStatus.Available)
            {                
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }

    //Function for pressing login button
    public void LoginButton()
    {
        //Start the login coroutine with entered email and password
        StartCoroutine(Login(L_Email.text, L_Password.text));
    }
    //Function for pressing signup button
    public void RegisterButton()
    {
        //Start the Signup coroutine with email, password, and fullname
        StartCoroutine(Register(S_Email.text, S_Password.text, S_Name.text));
    }

    private IEnumerator Login(string L_email, string L_password)
    {
        //Calling the Firebase auth signin function with entered email and password
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(L_email, L_password);

        //Waiting till response from firebase
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            //error message for different cases
            string message = "";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            L_ErrorText.text = message;
        }
        else
        {
            //Login successfull
            User = LoginTask.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            L_ErrorText.text = "";

            //Function called after successfull login
            uiscript.Display_Name = User.DisplayName;
            uiscript.Display_Email = User.Email;
            uiscript.AuthSuccess();
            
        }
    }

    private IEnumerator Register(string S_email, string S_password, string S_name)
    {
        if (S_name == "")
        {
            //If name input field is empty
            S_ErrorText.text = "Username missing";
        }
        else if (S_Password.text != S_ConfirmPass.text)
        {
            //If password does not match with conform password
            S_ErrorText.text = "Password Does Not Match..!";
        }
        else
        {
            //Calling the Firebase auth register function with entered email and password
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(S_email, S_password);

            //Wait for previous task to complete
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                //error message for different cases
                string message = "";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                S_ErrorText.text = message;
            }
            else
            {
                //User signedup                
                User = RegisterTask.Result.User;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = S_name };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    Task ProfileTask = User.UpdateUserProfileAsync(profile);

                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        S_ErrorText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set                                             
                        S_ErrorText.text = "";
                        print("Account Created");

                        //Function called after successful registration
                        uiscript.Display_Name = S_name;
                        uiscript.Display_Email = S_email;
                        uiscript.AuthSuccess();
                    }
                }
            }
        }
    }
}