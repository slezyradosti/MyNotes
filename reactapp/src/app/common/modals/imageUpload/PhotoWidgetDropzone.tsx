import { useCallback } from "react";
import { useDropzone } from "react-dropzone";
import { Button, Icon } from "semantic-ui-react";

interface Props {
    loading: boolean;
    uploadPhoto: (file: Blob) => void;
    // add callback function which will trigger Note update with adding link for the new image to the record
    // decided to remove callback
}

function MyDropZone({ loading, uploadPhoto }: Props) {

    // change for getting the only file
    const onDrop = useCallback((acceptedFiles: Blob[]) => {
        acceptedFiles.forEach((file: Blob) => {
            uploadPhoto(file)
        });

    }, [uploadPhoto]);

    const { getRootProps, getInputProps, isDragActive } = useDropzone({
        onDrop, accept: {
            'image/png': ['.png'],
            'image/jpg': ['.jpg'],
            'image/jpeg': ['.jpeg']
        }
    });

    return (
        <div {...getRootProps()}>
            <input {...getInputProps()} />
            {
                isDragActive ?
                    <p>Drop the picture here</p> :
                    //<Button className="addImageBtn" loading={loading} content='Add image' />
                    <Icon className="addImageIcon" name="file image" size='big' title='Add Image' color="black" inverted />
            }
        </div>
    );
}

export default MyDropZone;