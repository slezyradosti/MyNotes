import { useEffect, useState } from "react";
import { Button, Container, } from "semantic-ui-react";
import { Notebook } from "../models/notebook";
import NavBar from "./NavBar";
import NotebookDashboard from "../../features/notebooks/dashboard/NotebookDashboard";
import { v4 as uuid } from 'uuid';
import agent from "../api/agent";
import LoadingComponent from "./LoadingComponent";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";

function App() {
  const { notebookStore } = useStore();

  const [notebooks, setNotebooks] = useState<Notebook[]>([]);
  const [selectedNotebook, setSelectedNotebook] = useState<Notebook | undefined>(undefined);
  const [editMode, setEditMode] = useState(false);
  const [ifSumbitting, setIfSubmitting] = useState(false);

  useEffect(() => {
    notebookStore.loadNotebooks();
  }, [notebookStore]);

  function handleCreateOrEditNotebook(notebook: Notebook) {
    setIfSubmitting(true);

    if (notebook.id) {
      agent.Notebooks.update(notebook).then(() => {
        setNotebooks([...notebooks.filter(x => x.id !== notebook.id), notebook]);
        setSelectedNotebook(notebook);
        setEditMode(false);
        setIfSubmitting(false);
      })
    }
    else {
      notebook.id = uuid();
      agent.Notebooks.create(notebook).then(() => {
        setNotebooks([...notebooks, notebook]);
        setSelectedNotebook(notebook);
        setEditMode(false);
        setIfSubmitting(false);
      })
    }
  }

  function handleDeleteNotebook(id: string) {
    setIfSubmitting(true);
    agent.Notebooks.delete(id).then(() => {
      setNotebooks([...notebooks.filter(x => x.id !== id)]);
      setIfSubmitting(false);
    })
  }

  if (notebookStore.laoadingInital) return <LoadingComponent content='Loading app' />

  return (
    <>
      <NavBar />
      <Container style={{ marginTop: '7em' }}>
        <NotebookDashboard
          notebooks={notebookStore.notebooks}
          createOrEdit={handleCreateOrEditNotebook}
          deleteNotebook={handleDeleteNotebook}
          ifSubmitting={ifSumbitting}
        />
      </Container>
    </>
  );
}

export default observer(App);
