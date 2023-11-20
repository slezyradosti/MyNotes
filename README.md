# MyNotes
Web-application for making notes "MyNotes"

# Links

http://mynotes.somee.com

http://www.mynotes.somee.com

# Technologies

- Backend: ASP.NET Core, Entity Framework Core, Identity Framework, MediatR
- Frontend: React (TypeScript) + MobX, Axios
- Database: MSSQL

# Features
- Registration and Login
- Get, Create, Edit and Delete notes (including notebooks, units, pages, notes).
- Add and Delete pictures to notes

# Brief look

![](gif link)

<details>
<summary>
  
# How to use

</summary>

To use web-site features, a user must be registered.
To register, a user must provide his email and name, create a nickname and a password.
To log in, a user must know his email and password.

To create a Notebook, a user needs to click “Menu” icon on top of the website. The sidebar will appear.
There is a button “Add Notebook” at the bottom of the sidebar. By clicking the button, a user will see a form for creating a Notebook. A Notebook name is required to fill. Then a user needs to click a button, “Submit” to create a notebook or “Cancel” to cancel the action.

When a Notebook is created, a user can edit and delete a Notebook by click arrow beside its name. To open the Notebook, a user needs to lick on its name. The Units of the Notebooks are loaded then. A User can manipulate with Units the same way as Notebook.

Then a user can open a created Unit. The Pages of the Unit are loaded then. A User can manipulate with Pages the same way as Units.

To add a Note, a user needs to open a Page. The Notes of the Page are loaded then. Notes are showed on the main screen, not inside the sidebar.
To add a Note, a user needs to click button '+' at the top of the screen. User will see creating form. Note name and record are required to fill.
Then to create the Note, a user needs to press “Submit” button.
A user can add pictures to his notes. Better way to adding picture is edition a Note.
To edit a Note, a user needs to click its name or its record. The editing form will appear. To add an image, a user needs to click 'Add Image' icon (names of the icons appear on hover). The uploading image form will appear. A user just needs to select an image to upload and the uploading process will start. Loading indicator on the button “Submit” will indicate that the Note is uploading to a server. Eventually, a user will see the Note with uploaded image.
To delete an Image, a user can click images icon or just delete link to the image inside the Note's record. After clicking the images icon, a list of the Note's images will appear with a “trash” icon beside each. To delete an image, a user needs to click the “trash” button.

A User can style Note's record. There is “Help” button with an '?' icon available during the editing note process. After licking on the button a user will see styling instructions.

Users can navigate to their profiles by licking on their name and choosing “My profile” options. “My profile” page shows users their statistic of creating notebooks and notes during a current year.
Users can log out by clicking their name and choosing “Logout”.
  
</details>


# Database Schema

![](picture link)

