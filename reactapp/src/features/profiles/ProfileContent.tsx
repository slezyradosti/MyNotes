import { Tab } from "semantic-ui-react";
import ProfileNotebookStatistic from "./ProfileNotebookStatistic";
import ProfileNoteStatistic from "./ProfileNoteStatistic";

function ProfileContent() {
    const panes = [
        { menuItem: 'Notebooks Statistic', render: () => <Tab.Pane> <ProfileNotebookStatistic /> </Tab.Pane> },
        { menuItem: 'Notes Statistic', render: () => <Tab.Pane> <ProfileNoteStatistic /> </Tab.Pane> }
    ]

    return (
        <Tab
            menu={{ fluid: true, vertical: true }}
            menuPosition='right'
            panes={panes}
        />
    );
}

export default ProfileContent;