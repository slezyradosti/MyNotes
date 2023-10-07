import { SyntheticEvent } from "react";
import { Dropdown } from "semantic-ui-react";
import { Notebook } from "../../models/notebook";
import { Unit } from "../../models/unit";
import { Page } from "../../models/page";

interface Props {
    entity: Notebook | Unit | Page;
    entityLoading: boolean;
    target: string;

    handleDeleteEntity(e: SyntheticEvent<HTMLButtonElement>, id: string): void;
    handleNameEditStart: (entityId: string, currentName: string) => void;
}

function SidenavListDropdown({ entity, entityLoading,
    handleDeleteEntity, handleNameEditStart, target }: Props) {
    return (
        <Dropdown
            placeholder=" "
            fluid
            className="ui selection"
            style={{ color: '#a0a0a0', backgroundColor: 'transparent', border: 'none' }}
        >
            <Dropdown.Menu style={{ backgroundColor: 'transparent', right: 0, top: 15, border: 'none' }}>
                <Dropdown.Item
                    key={`edit ${entity.id}`}
                    style={{ color: '#a0a0a0', cursor: 'pointer', border: 'none' }}
                    content='Edit'
                    onClick={() => handleNameEditStart(entity.id!, entity.name)}
                >
                </Dropdown.Item>
                <Dropdown.Item style={{ color: '#a0a0a0', cursor: 'pointer', border: 'none' }}
                    key={`delete ${entity.id}`}
                    name={entity.id}
                    loading={entityLoading && (target === entity.id)}
                    onClick={(e) => handleDeleteEntity(e, entity.id!)}
                    content='Delete'
                >
                </Dropdown.Item>
            </Dropdown.Menu>
        </Dropdown>
    );
}

export default SidenavListDropdown;