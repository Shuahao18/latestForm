Imports System.IO

Public Class Form1

    ' Define a dictionary to store user information
    Private users As New Dictionary(Of String, UserInfo)

    ' UserInfo class to store user details
    Private Class UserInfo
        Public Property FirstName As String
        Public Property LastName As String
        Public Property Phone As String
        Public Property Username As String
        Public Property Password As String
    End Class

    ' Registration button click event handler
    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        ' Check if passwords match
        If txtReenterPassword.Text <> txtPassword.Text Then
            MessageBox.Show("Passwords do not match. Please re-enter password.")
            Return
        End If

        ' Check if username already exists
        If users.ContainsKey(txtPhone.Text) Then
            MessageBox.Show("Username already exists. Please choose another username.")
            Return
        End If

        ' Create a new UserInfo object and store user details
        Dim newUser As New UserInfo()
        newUser.FirstName = txtFirstname.Text
        newUser.LastName = txtLastname.Text
        newUser.Phone = txtPhone.Text
        newUser.Username = txtUsername.Text
        newUser.Password = txtReenterPassword.Text

        ' Add user to dictionary
        users.Add(newUser.Username, newUser)

        MessageBox.Show("Registration successful!")
        txtFirstname.Clear()
        txtLastname.Clear()
        txtPhone.Clear()
        txtUsername.Clear()
        txtPassword.Clear()
        txtReenterPassword.Clear()


    End Sub

    ' Login button click event handler
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ' Check if username exists
        If users.ContainsKey(txtLoginUsername.Text) Then
            ' Check if password matches
            If users(txtLoginUsername.Text).Password = txtLoginPassword.Text Then
                MessageBox.Show("Login successful!")

                ' Hide the login section
                login.Hide()
                ' Show the Change Account section
                ChangeAccount.Show()
                ' Allow the user to log in with the updated account information
                txtLoginUsername.Text = users(txtLoginUsername.Text).Username
                txtLoginPassword.Text = users(txtLoginUsername.Text).Password
            Else
                MessageBox.Show("Invalid password. Please try again.")
            End If
        Else
            MessageBox.Show("Username not found. Please register.")
        End If

    End Sub

    ' Change account button click event handler
    Private Sub btnChangeAccount_Click(sender As Object, e As EventArgs) Handles btnChangeAccount.Click
        ' Check if all fields are filled
        If String.IsNullOrWhiteSpace(updatedUsername.Text) OrElse String.IsNullOrWhiteSpace(updatedPassword.Text) OrElse String.IsNullOrWhiteSpace(updatednewPassword.Text) Then
            MessageBox.Show("Please fill in all fields.")
            Return
        End If

        ' Check if passwords match
        If updatedPassword.Text <> updatednewPassword.Text Then
            MessageBox.Show("New passwords do not match. Please re-enter password.")
            Return
        End If

        ' Retrieve user information
        Dim userInfo As UserInfo = users(txtLoginUsername.Text)

        ' Check if provided information matches registered information
        If userInfo.FirstName = txtNewFirstName.Text AndAlso
        userInfo.LastName = txtNewLastName.Text AndAlso
        userInfo.Phone = txtNewPhone.Text AndAlso
        userInfo.Username = txtNewUsername.Text AndAlso
        userInfo.Password = txtNewPassword.Text Then

            ' Confirm the changes with the user
            Dim confirmResult As DialogResult = MessageBox.Show("Are you sure you want to change your account information?", "Confirm Changes", MessageBoxButtons.YesNo)

            ' If user confirms, proceed with the changes
            If confirmResult = DialogResult.Yes Then
                ' Update user information
                userInfo.Username = updatedUsername.Text
                userInfo.Password = updatedPassword.Text
                txtNewFirstName.Clear()
                txtNewLastName.Clear()
                txtNewPhone.Clear()
                txtNewUsername.Clear()
                txtNewPassword.Clear()

                updatedUsername.Clear()
                updatedPassword.Clear()
                updatednewPassword.Clear()

                ' Update the username in the dictionary
                users(userInfo.Username) = userInfo

                MessageBox.Show("Account information updated successfully!")

                ' Update the username and password for login
                txtLoginUsername.Text = updatedUsername.Text
                txtLoginPassword.Text = updatedPassword.Text
            End If
        Else
            MessageBox.Show("Provided information does not match registered information.")
        End If
    End Sub

    ' Recover account button click event handler
    Private Sub btnRecover_Click(sender As Object, e As EventArgs) Handles btnRecover.Click
        ' Check if all fields are filled
        If String.IsNullOrWhiteSpace(txtRecoverFirstName.Text) OrElse String.IsNullOrWhiteSpace(txtRecoverLastName.Text) OrElse String.IsNullOrWhiteSpace(txtRecoverPhone.Text) Then
            MessageBox.Show("Please enter all the information to recover the account.")
            Return
        End If

        ' Search for user information based on first name
        Dim foundUser As UserInfo = Nothing
        For Each user In users.Values
            If user.FirstName = txtRecoverFirstName.Text AndAlso user.LastName = txtRecoverLastName.Text AndAlso user.Phone = txtRecoverPhone.Text Then
                foundUser = user
                Exit For
            End If
        Next

        ' Check if user is found
        If foundUser IsNot Nothing Then
            MessageBox.Show($"Username: {foundUser.Username}{Environment.NewLine}Password: {foundUser.Password}", "Account Details")
            txtRecoverFirstName.Clear()
            txtRecoverLastName.Clear()
            txtRecoverPhone.Clear()
        Else
            MessageBox.Show("Account not found. Please check your information.")
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        login.Show()
        ChangeAccount.Hide()
        register.Hide()
        recoverAcc.Hide()
    End Sub

    Private Sub lnkRegister_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles registerLink.LinkClicked
        login.Hide()
        ChangeAccount.Hide()
        register.Show()
    End Sub

    Private Sub lnkLogin_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles loginLink.LinkClicked
        register.Hide()
        ChangeAccount.Hide()
        login.Show()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        register.Hide()
        ChangeAccount.Hide()
        login.Show()
        recoverAcc.Hide()
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        register.Hide()
        ChangeAccount.Hide()
        login.Hide()
        recoverAcc.Show()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        register.Hide()
        ChangeAccount.Hide()
        login.Show()
        recoverAcc.Hide()
    End Sub
    Private Sub regCancel_Click(sender As Object, e As EventArgs) Handles regCancel.Click, loginCancel.Click, recoverCancel.Click, changeCancel.Click
        txtFirstname.Clear()
        txtLastname.Clear()
        txtPhone.Clear()
        txtUsername.Clear()
        txtPassword.Clear()
        txtReenterPassword.Clear()

        txtLoginUsername.Clear()
        txtLoginPassword.Clear()

        txtRecoverFirstName.Clear()
        txtRecoverLastName.Clear()
        txtRecoverPhone.Clear()

        txtNewFirstName.Clear()
        txtNewLastName.Clear()
        txtNewPhone.Clear()
        txtNewUsername.Clear()
        txtNewPassword.Clear()

        updatedUsername.Clear()
        updatedPassword.Clear()
        updatednewPassword.Clear()
    End Sub


End Class
