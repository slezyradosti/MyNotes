import { observer } from "mobx-react-lite";
import { CartesianGrid, Line, LineChart, ResponsiveContainer, Tooltip, XAxis, YAxis } from "recharts";

interface Props {
    data: any[];
}

function StatisticLineChart({ data }: Props) {
    return (
        <ResponsiveContainer width="95%" height={450}>
            <LineChart width={600} height={450} data={data} margin={{ top: 5, right: 20, left: 10, bottom: 5 }}>

                <Tooltip />
                <CartesianGrid stroke="#f5f5f5" />
                <Line type="monotone" dataKey="count" stroke="#4b9fed" yAxisId={0} />
                <XAxis dataKey="date" />
                <YAxis />

            </LineChart>
        </ResponsiveContainer >
    );
}

export default observer(StatisticLineChart);