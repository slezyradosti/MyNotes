import { observer } from 'mobx-react-lite';
import { Grid, Header, Label } from 'semantic-ui-react';

function HelpInfo() {
    return (
        <>
            <Header style={{ textAlign: 'center' }}>Uploading images</Header>
            <Label>You can upload your own images only in Edit mode</Label>
            <Header style={{ textAlign: 'center' }}>How to style my record</Header>
            <Grid columns={2} celled>
                <Grid.Row>
                    <Grid.Column>
                        <Header>
                            Type
                        </Header>
                    </Grid.Column>
                    <Grid.Column>
                        <Header>
                            Or
                        </Header>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column>*Italic*</Grid.Column>
                    <Grid.Column >_Italic_</Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column>**Bold**</Grid.Column>
                    <Grid.Column >__Bold__</Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column ># Heading</Grid.Column>
                    <Grid.Column >
                        <p>Heading 1</p>
                        <p>=========</p>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column >## Heading 2</Grid.Column>
                    <Grid.Column >
                        <p>Heading 2</p>
                        <p>---------</p>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column>{"[Link](http://a.com)"}</Grid.Column>
                    <Grid.Column>
                        <p>{"[Link][1]"}</p>
                        <p>:</p>
                        <p>{"[1]: http://b.org"}</p>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column>{"![Image](http://url/a.png)"}</Grid.Column>
                    <Grid.Column>
                        <p>{"![Image][1]"}</p>
                        <p>:</p>
                        <p>{"[1]: http://url/b.jpg"}</p>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column>{"> Blockquote"}</Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column>
                        <p>{"* List"}</p>
                        <p>{"* List"}</p>
                        <p>{"* List"}</p>
                    </Grid.Column>
                    <Grid.Column>
                        <p>{"- List"}</p>
                        <p>{"- List"}</p>
                        <p>{"- List"}</p>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column>
                        <p>Horizontal rule:</p>
                        <p></p>
                        <p>---</p>
                    </Grid.Column>
                    <Grid.Column>
                        <p>Horizontal rule:</p>
                        <p></p>
                        <p>***</p>
                    </Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column>{"`Inline code` with backticks"}</Grid.Column>
                </Grid.Row>
                <Grid.Row>
                    <Grid.Column>
                        <p>{"```"}</p>
                        <p>{"# code block"}</p>
                        <p>{"print '123'"}</p>
                        <p>{"console.log('12')"}</p>
                        <p>{"```"}</p>
                    </Grid.Column>
                    <Grid.Column>
                        <p>{"····# code block"}</p>
                        <p>{"····print '3 backticks or'"}</p>
                        <p>{"····print 'indent 4 spaces'"}</p>
                    </Grid.Column>
                </Grid.Row>
            </Grid>
        </>
    );
}

export default observer(HelpInfo);