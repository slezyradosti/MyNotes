import Markdown from "react-markdown";
import { Button, Header, Icon, Item } from "semantic-ui-react";
import Note from "../../../app/models/note";
import { SyntheticEvent } from "react";
import remarkGfm from "remark-gfm";

interface Props {
    note: Note;
    noteLoading: boolean;
    noteSelectedElement: Note | undefined;
    noteEditMode: boolean;
    handleEditNoteStart: (noteId: string) => void;
    handleDeleteNote: (e: SyntheticEvent<HTMLButtonElement>, noteId: string) => void;

}

function NoteListContent({ note, noteLoading, noteEditMode,
    noteSelectedElement, handleEditNoteStart, handleDeleteNote }: Props) {
    return (
        <div>
            {/* Use a label as a clickable element */}
            <div className="nameBtn">
                <Header
                    onClick={() => handleEditNoteStart(note.id!)}
                    style={{ cursor: "pointer" }}
                    className="Header"
                >
                    {note.name}
                </Header>
                <Button
                    loading={noteLoading && noteSelectedElement?.id === note.id && !noteEditMode}
                    onClick={(e) => handleDeleteNote(e, note.id!)}
                    style={{ backgroundColor: 'transparent' }}
                >
                    <Icon name='trash' />
                </Button>
            </div>
            {/* Use a div for displaying the description */}
            <Item.Meta style={{ color: '#808080' }}>{note.createdAt}</Item.Meta>
            <label
                className="markwonLabelCodeSyles"

                onClick={() => handleEditNoteStart(note.id!)}
            >
                <Markdown
                    components={{ img: ({ node, ...props }) => <img className='markdownImgBox' {...props} /> }}

                    remarkPlugins={[[remarkGfm, { whiteSpace: 'pre-wrap' }]]}
                    disallowedElements={['pre']} // disallowedElements (Array<string>, default: []) — tag names to disallow; cannot combine w/ allowedElements
                    unwrapDisallowed={true} // unwrapDisallowed (boolean, default: false) — extract (unwrap) what’s in disallowed elements; normally when say strong is not allowed, it and it’s children are dropped, with unwrapDisallowed the element itself is replaced by its children
                >
                    {note.record}
                </Markdown>
            </label>

        </div>
    );
}

export default NoteListContent;