using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yodii.Script.Debugger
{
    /// <summary>
    /// Basic class parsing an <see cref="Expr"/> into a list of atomic breakable <see cref="Expr"/>
    /// </summary>
    public class BreakableVisitor : ExprVisitor
    {
        readonly List<List<Expr>> _breakableExprs = new List<List<Expr>>();
        /// <summary>
        /// Reads the full AST, to find all breakable atomic <see cref="Expr"/>
        /// </summary>
        /// <param name="e">An Expr to parse as Breakables Exprs</param>
        /// <returns></returns>
        public override Expr VisitExpr( Expr e )
        {
            while( _breakableExprs.Count <= e.Location.Line )
            {
                _breakableExprs.Add( null );
            }
            if( _breakableExprs[e.Location.Line] == null )
            {
                _breakableExprs[e.Location.Line] = new List<Expr>();
                
                
            }
            if( e.IsBreakable )
            {
                _breakableExprs[e.Location.Line].Add( e );
            }
            return base.VisitExpr( e );
        }
        public List<List<Expr>> BreakableExprs
        {
            get { return _breakableExprs; }
        }
    }
}
