import axios from "axios";
import { useEffect, useState } from "react";
import { Header, List } from "semantic-ui-react";
import { Notebook } from "../models/notebook";

function App() {
  const [notebooks, setNotebooks] = useState<Notebook[]>([]);
  const config = {
    headers: { Authorization : 'Bearer ' + 'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImphY2siLCJuYW1laWQiOiJkN2YwYmExOS03ZWVmLTRiYjAtMWQxNC0wOGRiYTM2MTlmMGMiLCJlbWFpbCI6ImphY2tAdGFjay5jb20iLCJuYmYiOjE2OTQ5NzA0MDQsImV4cCI6MTY5NDk3MDcwNCwiaWF0IjoxNjk0OTcwNDA0fQ.zgoQEAguwWFXVMDidGkxoG0RXHvxa5E9B57vS6woJ3MB8Nx9PALAqTQm-ReH6GKFgEic9RsPsefhJTlnE_I0_w' }
  };

  useEffect(() => {
    axios
      .get<Notebook[]>('https://localhost:7177/Notebooks', config)
      .then((response) => {
        setNotebooks(response.data);
      });
  }, []);

  return (
    <>
      <Header as="h2" icon="users" contet="MyNotes" />
      <List>
        {notebooks.map((notebook) => (
          <List.Item key={notebook.id}>{notebook.name}</List.Item>
        ))}
      </List>
    </>
  );
}

export default App;
