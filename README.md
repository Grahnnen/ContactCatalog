# ContactCatalog
A simple contact catalog. <br>
This Projects uses xUnit and Moq to test if an invalid contact throws an error and check if email validation works.

<img width="1103" height="619" alt="ContactCatalog" src="https://github.com/user-attachments/assets/ba0d575e-c2c5-4f29-825d-151907ba5d91" />

## Project Structure

- The application starts in `Main()` which calls `ui.MainMenu()`
- `MainUiService` manages the main menu loop and handles all user interface interactions.
- UI methods delegate logic to `ContactService` which is responsible for creating, validating, filtering, and managing contacts.

## Usage
Seperate Tags with comma(,).
<br>Export to CSV exports to "ContactCatalog\bin\Debug\net8.0" with the filename and extenstion typed by the user.




