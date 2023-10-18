import { Tab } from "semantic-ui-react";

function ProfileContent() {
    const panes = [
        { menuItem: 'About', render: () => <Tab.Pane>Abount Content</Tab.Pane> },
        { menuItem: 'Photos', render: () => <Tab.Pane>Photos Content</Tab.Pane> },
        { menuItem: 'Statistic', render: () => <Tab.Pane>Statistic Content</Tab.Pane> }
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