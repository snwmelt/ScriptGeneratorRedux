using System;

namespace ScriptGeneratorRedux.Models.Core.Authentication.Interfaces
{
    internal interface IUserAuthenticator
    {
        /// <summary>
        /// Attempt to authenticate user.
        /// </summary>
        /// <param name="Username">System.String Containing User Username.</param>
        /// <param name="Password">System.String Containing User Password.</param>
        void Authenticate( String Username, String Password );

        /// <summary>
        /// Registers user for future authentication.
        /// </summary>
        /// <param name="Username">System.String Containing User Username.</param>
        /// <param name="Password">System.String Containing User Password.</param>
        /// <returns>True If User Has Been Successfully Registered.</returns>
        Boolean Create( String Username, String Password );

        /// <summary>
        /// Releases currently authenticated user.
        /// </summary>
        void DeAuthenticate( );

        /// <summary>
        /// True if IUserAuthenticator has a currently authenticated user.
        /// </summary>
        Boolean HasAuthenticatedUser
        {
            get;
        }

        /// <summary>
        /// Checks if System.String is an acceptably formatted password string.
        /// </summary>
        /// <param name="Password">System.String To Inspect.</param>
        /// <returns>True If Passed System.String Is An Acceptably Formatted Password String.</returns>
        Boolean IsAValidPassword( String Password );

        /// <summary>
        /// Checks if System.String is an acceptably formatted username string.
        /// </summary>
        /// <param name="Password">System.String To Inspect.</param>
        /// <returns>True If Passed System.String Is An Acceptably Formatted Username String.</returns>
        Boolean IsAValidUsername( String Username );

        /// <summary>
        /// Checks if System.String identifies an already registered username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>True If System.String Identifies An Already Registered Username.</returns>
        Boolean UserExists( String username );

        /// <summary>
        /// Contains assigned ID of currently authenticated user.
        /// </summary>
        String UserID
        {
            get;
        }

        /// <summary>
        /// Contains internally generated System.Byte[] representation of currently authenticated user key.
        /// </summary>
        Byte[ ] UserKey
        {
            get;
        }

        /// <summary>
        /// Contains passed in username of currently authenticated user.
        /// </summary>
        String Username
        {
            get;
        }
    }
}