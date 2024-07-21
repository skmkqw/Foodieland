import {getUserData} from "@/lib/authorization";

export default async function Home() {
    const userData = await getUserData();
    return (
    <main>
      <div className='container'>
          <h1>FoodieLand</h1>
          <p>{userData?.unique_name}</p>
          <p>{userData?.email}</p>
      </div>
    </main>
    );
}
