import React, { use, useEffect } from 'react'
import { useParams } from 'react-router'
interface Props {}

const Index: React.FC<Props> = () => {
  let params = useParams()
  const [allPosts, setAllPosts] = React.useState([])
  const [post, setPost] = React.useState<any>(null)
  useEffect(() => {
    fetch(`${process.env.API_ADDRESS}/api/post`)
      .then((res) => res.json())
      .then((data) => setAllPosts(data))
  }, [])

  useEffect(() => {
    if (params && params.id) {
      let selectedPost = allPosts.filter(
        (post: any) => post.postId == params.id,
      )[0]
      setPost(selectedPost)
    } else {
      setPost(allPosts[0])
    }
  }, [params, allPosts])

  console.log('REACT_APP_TEXT', process.env)

  return (
    <>
      {/* <!-- component --> */}
      <div className="max-w-screen-xl mx-auto">
        {/* <!-- header --> */}
        <header className="flex items-center justify-between py-2 border-b">
          <a
            href="#"
            className="px-2 lg:px-0 uppercase font-bold text-purple-800"
          >
            ALT - Agile Lan Tuscany
          </a>
          <ul className="inline-flex items-center">
            {allPosts &&
              allPosts.map((post: any, i) => (
                <li key={i} className="px-2 md:px-4">
                  <a
                    href={`/${post.postId}`}
                    className="text-gray-800 font-semibold hover:text-purple-900"
                  >
                    {post.title}
                  </a>
                </li>
              ))}
          </ul>
        </header>
        {post && (
          <main className="mt-10">
            <div
              className="mb-4 md:mb-0 w-full max-w-screen-md mx-auto relative"
              style={{ height: '24em' }}
            >
              <div
                className="absolute left-0 bottom-0 w-full h-full z-10"
                style={{
                  backgroundImage:
                    'linear-gradient(180deg,transparent,rgba(0,0,0,.7))',
                }}
              ></div>
              <img
                src="https://images.unsplash.com/photo-1493770348161-369560ae357d?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=2100&q=80"
                className="absolute left-0 top-0 w-full h-full z-0 object-cover"
              />
              <div className="p-4 absolute bottom-0 left-0 z-20">
                <a
                  href="#"
                  className="px-4 py-1 bg-black text-gray-200 inline-flex items-center justify-center mb-2"
                >
                  Nerdism
                </a>
                <h2 className="text-4xl font-semibold text-gray-100 leading-tight">
                  {post.title}
                </h2>
                <div className="flex mt-3">
                  <img
                    src="https://randomuser.me/api/portraits/men/97.jpg"
                    className="h-10 w-10 rounded-full mr-2 object-cover"
                  />
                  <div>
                    <p className="font-semibold text-gray-200 text-sm">
                      {process.env.REACT_APP_TEXT}
                    </p>
                    <p className="font-semibold text-gray-400 text-xs">
                      1 Dec 2025
                    </p>
                  </div>
                </div>
              </div>
            </div>
            <div className="px-4 lg:px-0 mt-12 text-gray-700 max-w-screen-md mx-auto text-lg leading-relaxed">
              <p className="pb-6">{post.content}</p>
            </div>
          </main>
        )}
        {/* <!-- main ends here -->

    <!-- footer --> */}
        <footer className="border-t mt-32 pt-12 pb-32 px-4 lg:px-0">
          <div className="flex">
            <div className="w-full md:w-1/3 lg:w-1/4">
              <h6 className="font-semibold text-gray-700 mb-4">Company</h6>
              <ul>
                <li>
                  <a href="" className="block text-gray-600 py-2">
                    Team
                  </a>
                </li>
                <li>
                  <a href="" className="block text-gray-600 py-2">
                    About us
                  </a>
                </li>
                <li>
                  <a href="" className="block text-gray-600 py-2">
                    Press
                  </a>
                </li>
              </ul>
            </div>

            <div className="w-full md:w-1/3 lg:w-1/4">
              <h6 className="font-semibold text-gray-700 mb-4">Content</h6>
              <ul>
                <li>
                  <a href="" className="block text-gray-600 py-2">
                    Blog
                  </a>
                </li>
                <li>
                  <a href="" className="block text-gray-600 py-2">
                    Privacy Policy
                  </a>
                </li>
                <li>
                  <a href="" className="block text-gray-600 py-2">
                    Terms & Conditions
                  </a>
                </li>
                <li>
                  <a href="" className="block text-gray-600 py-2">
                    Documentation
                  </a>
                </li>
              </ul>
            </div>
          </div>
        </footer>
      </div>
    </>
  )
}
Index.displayName = 'Index'

export default Index
