import { useCallback } from "react";
import { useDropzone } from "react-dropzone";
import { Button } from "semantic-ui-react";

interface Props {
    loading: boolean;
    uploadPhoto: (file: Blob) => void;
    // add callback function which will trigger Note update with adding link for the new image to the record
    uploadPhotoToRecord: () => void;
}

function MyDropZone({ loading, uploadPhoto, uploadPhotoToRecord }: Props) {

    // change for getting the only file
    const onDrop = useCallback((acceptedFiles: Blob[]) => {
        acceptedFiles.forEach((file: Blob) => {
            uploadPhoto(file)
            uploadPhotoToRecord();
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
                    //<p>Click or drag a picture here</p>
                    <Button loading={loading} content='Add image' />
            }
        </div>
    );
}

export default MyDropZone;