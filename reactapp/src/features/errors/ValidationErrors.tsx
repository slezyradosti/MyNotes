import { Message } from "semantic-ui-react";

interface Props {
    errors: string[] | null;
}

function ValidationErrors({ errors }: Props) {
    console.log('errors ' + errors);

    return (
        <Message negative>
            {errors && (
                <Message.List>
                    {errors.map((err: string, i) => (
                        <Message.Item key={i}>{err}</Message.Item>
                    ))}
                </Message.List>
            )}
        </Message>
    );
}

export default ValidationErrors;