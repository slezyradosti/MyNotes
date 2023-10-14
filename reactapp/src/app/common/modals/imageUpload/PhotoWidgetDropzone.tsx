import { useCallback } from "react";
import { useDropzone } from "react-dropzone";
import { Button } from "semantic-ui-react";

interface Props {
    loading: boolean;
    uploadPhoto: (file: Blob) => void;
}

function MyDropZone({ loading, uploadPhoto }: Props) {
    const onDrop = useCallback((acceptedFiles: Blob[]) => {
        acceptedFiles.forEach((file: Blob) => uploadPhoto(file));
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