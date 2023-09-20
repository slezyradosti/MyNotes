import { useEffect, useState } from "react";
import { Container, } from "semantic-ui-react";
import { Notebook } from "../models/notebook";
import NavBar from "./NavBar";
import NotebookDashboard from "../../features/notebooks/dashboard/NotebookDashboard";
import { v4 as uuid } from 'uuid';
import routerAgent from "../api/routerAgent";
import LoadingComponent from "./LoadingComponent";

function App() {
  const [notebooks, setNotebooks] = useState<Notebook[]>([]);
  const [selectedNotebook, setSelectedNotebook] = useState<Notebook | undefined>(undefined);
  const [editMode, setEditMode] = useState(false);
  const [generalLoading, setGeneralLoading] = useState(true);
  const [ifSumbitting, setIfSubmitting] = useState(false);

  useEffect(() => {
    routerAgent.Notebooks.list().then(response => {
      let notebooks: Notebook[] = [];
      response.forEach(notebook => {
        notebook.createdAt = notebook.createdAt?.split('T')[0];
        notebooks.push(notebook);
      })
      setNotebooks(notebooks);
      setGeneralLoading(false);
    });
  }, []);

  function handleSelectNotebook(id: string) {
    setSelectedNotebook(notebooks.find(x => x.id === id));
  }

  function handleCancelSelectedNotebook() {
    setSelectedNotebook(undefined);
  }

  function handleFormOpen(id?: string) {
    id ? handleSelectNotebook(id) : handleCancelSelectedNotebook();
    setEditMode(true);
  }

  function handleFormClose() {
    setEditMode(false);
  }

  function handleCreateOrEditNotebook(notebook: Notebook) {
    setIfSubmitting(true);

    if (notebook.id) {
      routerAgent.Notebooks.update(notebook).then(() => {
        setNotebooks([...notebooks.filter(x => x.id !== notebook.id), notebook]);
        setSelectedNotebook(notebook);
        setEditMode(false);
        setIfSubmitting(false);
      })
    }
    else {
      notebook.id = uuid();
      routerAgent.Notebooks.create(notebook).then(() => {
        setNotebooks([...notebooks, notebook]);
        setSelectedNotebook(notebook);
        setEditMode(false);
        setIfSubmitting(false);
      })
    }
  }

  function handleDeleteNotebook(id: string) {
    setNotebooks([...notebooks.filter(x => x.id !== id)]);
  }

  if (generalLoading) return <LoadingComponent content='Loading app' />

  return (
    <>
      <NavBar openForm={handleFormOpen} />
      <Container style={{ marginTop: '7em' }}>
        <NotebookDashboard
          notebooks={notebooks}
          selectedNotebok={selectedNotebook}
          selectNotebook={handleSelectNotebook}
          cancelSelectedNotebook={handleCancelSelectedNotebook}
          editMode={editMode}
          openForm={handleFormOpen}
          closeForm={handleFormClose}
          createOrEdit={handleCreateOrEditNotebook}
          deleteNotebook={handleDeleteNotebook}
          ifSubmitting={ifSumbitting}
        />
      </Container>
    </>
  );
}

export default App;
